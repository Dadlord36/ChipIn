using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Common;
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
        where TRequestResponseDataModel : class, TRequestResponseModelInterface
        where TRequestResponseModelInterface : class
        where TDataType : class
    {
        [Space(25f)] [SerializeField] protected int itemsPerPage;
        [SerializeField] protected byte maxCachedPagesCount;
        [SerializeField] protected byte pagesPortion;
        [SerializeField] protected UserAuthorisationDataRepository authorisationDataRepository;

        [NonSerialized] private PaginatedList<TDataType> _paginatedData = new PaginatedList<TDataType>();

        protected abstract string Tag { get; }

        private List<DisposableCancellationTokenSource> GoingTasksCancellationTokenSources { get; set; } =
            new List<DisposableCancellationTokenSource>();

        #region IPaginatedItemsListInfo Implementation

        public bool IsBusy { get; private set; }
        public int ItemsPerPage => itemsPerPage;
        public int TotalPages { get; private set; }
        public uint TotalItemsNumber { get; private set; }
        public uint LastPageItemsNumber { get; private set; }

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

        public async Task<IReadOnlyList<TDataType>> TryGetPageItems(uint pageNumber)
        {
            if (!PageIsValid(pageNumber)) return null;
            if (_paginatedData.PageExists(pageNumber)) return _paginatedData[pageNumber];

            try
            {
                await LoadAndStorePageItems(pageNumber);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            return _paginatedData.PageExists(pageNumber) ? _paginatedData[pageNumber] : null;
        }


        public async Task<List<TDataType>> TryGetItemsRange(uint startIndex, uint length)
        {
            var startPageNumber = CalculatePageNumberForGivenIndex(startIndex);
            var endPageNumber = CalculatePageNumberForGivenIndex(startIndex + length);

            if (!PageIsValid(startPageNumber) || !PageIsValid(endPageNumber)) return null;
            if (startPageNumber > TotalPages && endPageNumber > TotalPages) return null;

            var startingIndex = CalculatePageItemIndexFromQueueIndex(startPageNumber, startIndex);

            var pagesData = new List<TDataType>();

            for (var i = startPageNumber; i <= endPageNumber; i++)
            {
                if (!_paginatedData.PageExists(i))
                {
                    try
                    {
                        await LoadAndStorePageItems(i);
                    }
                    catch (Exception e)
                    {
                        LogUtility.PrintLogException(e);
                        throw;
                    }
                }

                pagesData.AddRange(_paginatedData[i]);
            }

            return ArrayUtility.GetRemainArrayItemsStartingWithIndex(pagesData, startingIndex, length);
        }

        public async Task<TDataType> TryGetItemWithIndex(uint i)
        {
            var pageNumber = CalculatePageNumberForGivenIndex(i);

            if (!PageIsValid(pageNumber)) return null;

            if (!_paginatedData.PageExists(pageNumber))
            {
                try
                {
                    await LoadAndStorePageItems(pageNumber);
                }
                catch (Exception e)
                {
                    LogUtility.PrintLogException(e);
                    throw;
                }
                if (!_paginatedData.PageExists(pageNumber))
                    return null;
            }

            var page = _paginatedData[pageNumber];
            var itemNumber = CalculatePageItemIndexFromQueueIndex(pageNumber, i);

            return itemNumber < page.Count ? page[itemNumber] : null;
        }

        public override async Task LoadDataFromServer()
        {
            try
            {
                const int initialPage = 1;
                var firstPageResponse = await CreateAndRegisterLoadPaginatedItemsTask(new PaginatedRequestData(initialPage, itemsPerPage))
                    .ConfigureAwait(false);

                bool CheckIfRequestIsSuccessful(BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse
                    response)
                {
                    if (response.Success) return true;
                    LogUtility.PrintLog(Tag, response.Error);
                    return false;
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
                TotalItemsNumber = (uint) ((TotalPages - 1) * itemsPerPage + latsPageItems.Count);
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
                    var pageNumber = (uint) ((IPaginatedResponse) responses[i].ResponseModelInterface).Paginated.Page;
                    var items = GetItemsFromResponseModelInterface(responses[i].ResponseModelInterface);

                    _paginatedData.FillPageWithItems(pageNumber, items);
                }

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

        private void RegisterAsyncTaskExecution(Task task, IEnumerable<DisposableCancellationTokenSource> cancellationTokenSources)
        {
            IsBusy = true;
            GoingTasksCancellationTokenSources.AddRange(cancellationTokenSources);
            _goingOnTask = task;
            _goingOnTask.ContinueWith(delegate { IsBusy = false; });
        }

        private int CalculatePageItemIndexFromQueueIndex(uint pageNumber, uint queueIndex)
        {
            return (int) (queueIndex - (pageNumber - 1) * itemsPerPage);
        }

        private void RegisterAsyncTaskExecution(Task task, DisposableCancellationTokenSource cancellationTokenSource)
        {
            RegisterAsyncTaskExecution(task, new[] {cancellationTokenSource});
        }

        private bool PageIsValid(uint pageNumber)
        {
            return pageNumber > 0 && pageNumber <= TotalPages;
        }

        private async Task LoadAndStorePageItems(uint pageNumber)
        {
            try
            {
                if (!PageIsValid(pageNumber)) return;

                var loadingTask = CreateAndRegisterLoadPaginatedItemsTask(new PaginatedRequestData((int) pageNumber, itemsPerPage)).ConfigureAwait
                    (false);
                var loadedItems = await loadingTask;
                var items = GetItemsFromResponseModelInterface(loadedItems.ResponseModelInterface);

                if (!loadedItems.Success || items.Count == 0) return;

                _paginatedData.FillPageWithItems(pageNumber, items);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private uint CalculatePageNumberForGivenIndex(uint itemIndex)
        {
            return (uint) (itemIndex / itemsPerPage) + 1;
        }

        private uint CalculatePageNumberForGivenIndex(int itemIndex)
        {
            return CalculatePageNumberForGivenIndex((uint) itemIndex);
        }

        protected abstract
            Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse> CreateLoadPaginatedItemsTask(
                out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData);

        protected abstract List<TDataType> GetItemsFromResponseModelInterface(TRequestResponseModelInterface responseModelInterface);

        private Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse>
            CreateAndRegisterLoadPaginatedItemsTask(PaginatedRequestData paginatedRequestData)
        {
            var task = CreateLoadPaginatedItemsTask(out var cancellationTokenSource, paginatedRequestData);
            RegisterAsyncTaskExecution(task, cancellationTokenSource);
            return task;
        }


        private Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse[]> CreateLoadItemsPagesTask(
            IReadOnlyList<int> pagesNumbers)
        {
            var tasks =
                new Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse>[pagesNumbers.Count];

            var cancellationTokenSources = new List<DisposableCancellationTokenSource>(pagesNumbers.Count);

            for (int i = 0; i < pagesNumbers.Count; i++)
            {
                tasks[i] = CreateLoadPaginatedItemsTask(out var cancellationTokenSource, new PaginatedRequestData(pagesNumbers[i], itemsPerPage));
                cancellationTokenSources.Add(cancellationTokenSource);
            }

            var finalTask = Task.WhenAll(tasks);
            RegisterAsyncTaskExecution(finalTask, cancellationTokenSources);
            return finalTask;
        }

        #endregion
    }
}