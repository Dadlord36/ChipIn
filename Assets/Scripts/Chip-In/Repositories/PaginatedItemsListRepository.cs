using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using Common.AsyncTasksManagement;
using DataModels.Common;
using DataModels.Interfaces;
using HttpRequests.RequestsProcessors;
using Repositories.Interfaces;
using Repositories.Remote;
using UnityEngine;
using Utilities;

namespace Repositories
{
    public abstract class PaginatedItemsListRepository<TDataType, TRequestResponseDataModel, TRequestResponseModelInterface> : RemoteRepositoryBase,
        IPaginatedItemsListRepository<TDataType>
        where TDataType : class
        where TRequestResponseDataModel : class, TRequestResponseModelInterface
        where TRequestResponseModelInterface : class
    {
        [Space(25f)] [SerializeField] protected int itemsPerPage;
        [SerializeField] protected byte maxCachedPagesCount;
        [SerializeField] protected byte pagesPortion;
        [SerializeField] protected UserAuthorisationDataRepository authorisationDataRepository;

        [NonSerialized] private PaginatedList<TDataType> _paginatedData = new PaginatedList<TDataType>();

        private TaskManager<uint> PagesLoadingTaskManager { get; } = new TaskManager<uint>();

        protected abstract string Tag { get; }

        private List<DisposableCancellationTokenSource> GoingTasksCancellationTokenSources { get; set; } =
            new List<DisposableCancellationTokenSource>();

        #region IPaginatedItemsListInfo Implementation

        public bool IsInitialized { get; private set; }
        public bool IsBusy { get; private set; }
        public int ItemsPerPage => itemsPerPage;
        public int TotalPages { get; private set; }
        public uint TotalItemsNumber { get; private set; }
        public uint LastPageItemsNumber { get; private set; }
        public uint GetCorrespondingToIndexPage(uint pageItemIndex) => CalculatePageNumberForGivenIndex(pageItemIndex);

        #endregion

        protected override void CancelOngoingTask()
        {
            base.CancelOngoingTask();
            CancelAllTasks();
        }

        #region Public Methods

        public void CancelAllTasks()
        {
            foreach (var tokenSource in GoingTasksCancellationTokenSources)
            {
                if (!tokenSource.IsDisposed)
                    tokenSource.Cancel();
            }

            GoingTasksCancellationTokenSources.Clear();
        }

        public Task<IReadOnlyList<TDataType>> CreateGetPageItemsTask(uint pageNumber)
        {
            if (!PageIsValid(pageNumber))
                throw new ArgumentOutOfRangeException($"Page {pageNumber} is not valid. Maybe repository was not initialized");

            if (_paginatedData.PageExists(pageNumber)) return Task.FromResult<IReadOnlyList<TDataType>>(_paginatedData[pageNumber]);

            var cancellationSource = new DisposableCancellationTokenSource();

            var taskToReturn = CreateLoadAndStorePageItemsTask(pageNumber).ContinueWith(delegate
            {
                return _paginatedData.PageExists(pageNumber)
                    ? _paginatedData[pageNumber] as IReadOnlyList<TDataType>
                    : throw new ArgumentOutOfRangeException($"Page {pageNumber.ToString()} is not valid");
            }, cancellationSource.Token);

            RegisterAsyncTaskExecution(taskToReturn, cancellationSource);

            return taskToReturn;
        }

        public Task<IReadOnlyList<TDataType>> CreateGetItemsRangeTask(uint startIndex, uint length)
        {
            var startPageNumber = CalculatePageNumberForGivenIndex(startIndex);
            var endPageNumber = CalculatePageNumberForGivenIndex(startIndex + length);

            if (!PageIsValid(startPageNumber))
                throw new ArgumentOutOfRangeException($"Page {startPageNumber} is not valid. Maybe repository was not initialized");

            var startingIndex = CalculatePageItemIndexFromQueueIndex(startPageNumber, startIndex);
            var pagesData = new List<TDataType>();

            uint[] pagesNumbersToGet;

            if (PageIsValid(endPageNumber))
            {
                pagesNumbersToGet = new uint[endPageNumber + 1 - startPageNumber];
                int index = 0;

                for (uint i = startPageNumber; i <= endPageNumber; i++)
                {
                    pagesNumbersToGet[index] = i;
                    index++;
                }
            }
            else
            {
                pagesNumbersToGet = new[] {startPageNumber};
            }

            var pageLoadingTasks = new List<Task>();

            for (var i = 0; i < pagesNumbersToGet.Length; i++)
            {
                if (_paginatedData.PageExists(pagesNumbersToGet[i])) continue;
                pageLoadingTasks.Add(CreateLoadAndStorePageItemsTask(pagesNumbersToGet[i]));
            }

            IReadOnlyList<TDataType> GetItemsFromPaginatedData()
            {
                for (int i = 0; i < pagesNumbersToGet.Length; i++)
                {
                    pagesData.AddRange(_paginatedData[pagesNumbersToGet[i]]);
                }

                return ArrayUtility.GetRemainArrayItemsStartingWithIndex(pagesData, startingIndex, length);
            }

            if (pageLoadingTasks.Count > 0)
            {
                return Task.WhenAll(pageLoadingTasks).ContinueWith(delegate { return GetItemsFromPaginatedData(); }, TaskContinuationOptions.OnlyOnRanToCompletion);
            }

            return Task.FromResult(GetItemsFromPaginatedData());
        }

        public Task<TDataType> CreateGetItemWithIndexTask(uint itemIndex)
        {
            var pageNumber = CalculatePageNumberForGivenIndex(itemIndex);
            if (!PageIsValid(pageNumber)) throw new ArgumentOutOfRangeException($"Page {itemIndex} is not valid");

            TDataType GetPageItem(uint inPageNumber, uint inIndex)
            {
                var page = _paginatedData[inPageNumber];
                var itemNumber = CalculatePageItemIndexFromQueueIndex(inPageNumber, inIndex);
                return page[itemNumber];
            }

            if (!_paginatedData.PageExists(pageNumber))
            {
                var cancellationSource = new DisposableCancellationTokenSource();
                var taskToReturn = CreateLoadAndStorePageItemsTask(pageNumber).ContinueWith(delegate
                {
                    if (!_paginatedData.PageExists(pageNumber))
                        throw new ArgumentOutOfRangeException($"Page {itemIndex} is not exists");
                    return GetPageItem(pageNumber, itemIndex);
                }, cancellationSource.Token, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext());
                RegisterAsyncTaskExecution(taskToReturn, cancellationSource);
                return taskToReturn;
            }

            return Task.FromResult(GetPageItem(pageNumber, itemIndex));
        }

        public override async Task LoadDataFromServer()
        {
            try
            {
                const int initialPage = 1;
                var firstPageResponse = await CreateAndRegisterLoadPaginatedItemsTask(new PaginatedRequestData(initialPage, itemsPerPage))
                    .ConfigureAwait(false);

                bool CheckIfRequestIsSuccessful(BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>
                    .HttpResponse response)
                {
                    if (!response.Success)
                        LogPageLoadingError(response);
                    return response.Success;
                }

                void LogPageLoadingError(BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse
                    response)
                {
                    LogUtility.PrintLogError(Tag, $"Failed to load initial repository data from server. Response Phrase: " +
                                                  $"{response.ResponsePhrase}, Error message: {response.Error}");
                }

                if (!CheckIfRequestIsSuccessful(firstPageResponse))
                {
                    return;
                }

                var responseModelInterface = firstPageResponse.ResponseModelInterface;
                var paginatedResponseInterface = responseModelInterface as IPaginatedResponse;

                Debug.Assert(paginatedResponseInterface != null, nameof(paginatedResponseInterface) + " != null");
                TotalPages = paginatedResponseInterface.Paginated.Total;

                var lastPageResponse = await CreateAndRegisterLoadPaginatedItemsTask(new PaginatedRequestData(TotalPages, itemsPerPage))
                    .ConfigureAwait(false);

                if (!CheckIfRequestIsSuccessful(lastPageResponse))
                {
                    return;
                }

                if (TotalPages <= 1)
                {
                    return;
                }

                var latsPageItems = GetItemsFromResponseModelInterface(lastPageResponse.ResponseModelInterface);
                TotalItemsNumber = (uint) (((TotalPages - 1) * itemsPerPage) + latsPageItems.Count);
                LastPageItemsNumber = (uint) latsPageItems.Count;

                _paginatedData.FillPageWithItems(initialPage, GetItemsFromResponseModelInterface(responseModelInterface));

                if (TotalPages - 1 < 1) return;

                var pagesToLoad = new int[TotalPages - 1];

                for (int i = 0; i < pagesToLoad.Length; i++)
                {
                    pagesToLoad[i] = initialPage + i + 1;
                }

                var responses = await CreateLoadItemsPagesTask(pagesToLoad).ConfigureAwait(false);

                for (int i = 0; i < responses.Length; i++)
                {
                    GetResponseItemsAndFillPaginatedData(responses[i].ResponseModelInterface);
                }

                IsInitialized = true;
                ConfirmDataLoading();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        #endregion

        private void OnEnable()
        {
            _paginatedData = new PaginatedList<TDataType>(maxCachedPagesCount, itemsPerPage);
        }

        #region Private Methods

        private Task _goingOnTask;

        private void GetResponseItemsAndFillPaginatedData(TRequestResponseModelInterface responseModelInterface)
        {
            var pageNumber = (uint) ((IPaginatedResponse) responseModelInterface).Paginated.Page;
            var items = GetItemsFromResponseModelInterface(responseModelInterface);

            _paginatedData.FillPageWithItems(pageNumber, items);
        }

        private void RegisterAsyncTaskExecution(Task task, IEnumerable<DisposableCancellationTokenSource> cancellationTokenSources)
        {
            IsBusy = true;
            GoingTasksCancellationTokenSources.AddRange(cancellationTokenSources);
            _goingOnTask = task;
            _goingOnTask.ContinueWith(delegate { IsBusy = false; });
        }

        private void RegisterAsyncTaskExecution(Task task, DisposableCancellationTokenSource cancellationTokenSource)
        {
            RegisterAsyncTaskExecution(task, new[] {cancellationTokenSource});
        }

        private int CalculatePageItemIndexFromQueueIndex(uint pageNumber, uint queueIndex)
        {
            return (int) (queueIndex - ((pageNumber - 1) * itemsPerPage));
        }

        private bool PageIsValid(uint pageNumber)
        {
            return pageNumber > 0 && pageNumber <= TotalPages;
        }

        private Task CreateLoadAndStorePageItemsTask(uint pageNumber)
        {
            if (!PageIsValid(pageNumber)) throw new Exception("Impossible page");

            var httpResponse = CreateAndRegisterLoadPaginatedItemsTask(new PaginatedRequestData((int) pageNumber,
                itemsPerPage)).ContinueWith(delegate(Task<BaseRequestProcessor<object, TRequestResponseDataModel,
                TRequestResponseModelInterface>.HttpResponse> task)
            {
                GetResponseItemsAndFillPaginatedData(task.Result.ResponseModelInterface);
            }, TaskContinuationOptions.OnlyOnRanToCompletion);

            return PagesLoadingTaskManager.RequestTask(pageNumber, httpResponse);
        }

        private uint CalculatePageNumberForGivenIndex(uint itemIndex)
        {
            return (uint) (itemIndex / itemsPerPage) + 1;
        }

        private uint CalculatePageNumberForGivenIndex(int itemIndex)
        {
            return CalculatePageNumberForGivenIndex((uint) itemIndex);
        }

        protected abstract Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse>
            CreateLoadPaginatedItemsTask(out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData);

        protected abstract List<TDataType> GetItemsFromResponseModelInterface(TRequestResponseModelInterface responseModelInterface);

        private Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse>
            CreateAndRegisterLoadPaginatedItemsTask(PaginatedRequestData paginatedRequestData)
        {
            var task = CreateLoadPaginatedItemsTask(out var cancellationTokenSource, paginatedRequestData);
            RegisterAsyncTaskExecution(task, cancellationTokenSource);
            return task;
        }

        private Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse[]>
            CreateLoadItemsPagesTask(IReadOnlyList<int> pagesNumbers)
        {
            var tasks = new Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>
                .HttpResponse>[pagesNumbers.Count];

            var cancellationTokenSources = new List<DisposableCancellationTokenSource>(pagesNumbers.Count);

            for (int i = 0; i < pagesNumbers.Count; i++)
            {
                tasks[i] = CreateLoadPaginatedItemsTask(out var cancellationTokenSource, new PaginatedRequestData
                    (pagesNumbers[i], itemsPerPage));
                cancellationTokenSources.Add(cancellationTokenSource);
            }

            var finalTask = Task.WhenAll(tasks);
            RegisterAsyncTaskExecution(finalTask, cancellationTokenSources);
            return finalTask;
        }

        #endregion
    }
}