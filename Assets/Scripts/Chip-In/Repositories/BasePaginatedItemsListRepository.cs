using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels.Common;
using DataModels.Interfaces;
using HttpRequests.RequestsProcessors;
using Repositories.Remote;
using UnityEngine;
using Utilities;

namespace Repositories
{
    public abstract class BasePaginatedItemsListRepository<TDataType, TRequestResponseDataModel,
        TRequestResponseModelInterface> : RemoteRepositoryBase
        where TRequestResponseDataModel : class, TRequestResponseModelInterface
        where TRequestResponseModelInterface : class
        where TDataType : class
    {
        [Space(25f)] [SerializeField] protected int itemsPerPage;
        [SerializeField] protected byte maxCachedPagesCount;
        [SerializeField] protected byte pagesPortion;
        [SerializeField] protected UserAuthorisationDataRepository authorisationDataRepository;

        [NonSerialized] private PaginatedList<TDataType> _paginatedData = new PaginatedList<TDataType>();

        /*private uint CurrentPage { get; set; } = 1;*/

        protected abstract string Tag { get; }

        private short _pagesProgress;
        public int ItemsPerPage => itemsPerPage;
        public int TotalPages { get; private set; }
        public uint TotalItemsNumber { get; private set; }
        public uint LastPageItemsNumber { get; private set; }

        #region Public Methods

        /*public bool TryGetNextListPage(out List<TDataType> items)
        {
            if (_paginatedData.TryGetNextPageItems(out items))
            {
                return true;
            }
            else
            {
                LoadPageItems();
                return _paginatedData.TryGetNextPageItems(out items);
            }
        }*/


        public async Task<IReadOnlyList<TDataType>> TryGetPageItems(uint pageNumber)
        {
            if (!_paginatedData.PageExists(pageNumber))
            {
                await LoadAndStorePageItems(pageNumber);
            }

            return _paginatedData[pageNumber];
        }

        public async Task<List<TDataType>> TryGetItemsRange(uint startIndex, uint length)
        {
            var startPageNumber = CalculatePageNumberForGivenIndex(startIndex);
            var endPageNumber = CalculatePageNumberForGivenIndex(startIndex + length);

            var startingIndex = CalculatePageItemIndexFromQueueIndex(startPageNumber, startIndex);

            if (startPageNumber > TotalPages && endPageNumber > TotalPages) return null;

            if (endPageNumber > TotalPages)
            {
                var pageItems = _paginatedData[startPageNumber];
                return pageItems.GetRange(startingIndex, pageItems.Count - 1 - startingIndex);
            }

            var pagesData = new List<TDataType>();

            for (var i = startPageNumber; i <= endPageNumber; i++)
            {
                if (!_paginatedData.PageExists(i))
                {
                    await LoadAndStorePageItems(i);
                }

                pagesData.AddRange(_paginatedData[i]);
            }

            return ArrayUtility.GetRemainArrayItemsStartingWithIndex(pagesData, startingIndex, length);
        }

        public Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse
            >
            TryLoadItemsPages(uint pageNumber)
        {
            return CreateLoadPaginatedItemsTask(new PaginatedRequestData((int) pageNumber, itemsPerPage));
        }

        public async Task<TDataType> TryGetItemWithIndex(uint i)
        {
            var pageNumber = CalculatePageNumberForGivenIndex(i);

            if (pageNumber > TotalPages) return null;

            if (!_paginatedData.PageExists(pageNumber))
            {
                await LoadAndStorePageItems(pageNumber);
            }

            var page = _paginatedData[pageNumber];
            var itemNumber = CalculatePageItemIndexFromQueueIndex(pageNumber, i);

            return itemNumber < page.Count ? page[itemNumber] : null;
        }
        
        private int CalculatePageItemIndexFromQueueIndex(uint pageNumber, uint queueIndex)
        {
            return (int) (queueIndex - (pageNumber - 1) * itemsPerPage);
        }

        public override async Task LoadDataFromServer()
        {
            try
            {
                const int initialPage = 1;
                var firstPageResponse =
                    await CreateLoadPaginatedItemsTask(new PaginatedRequestData(initialPage, itemsPerPage));

                bool CheckIfRequestIsSuccessful(
                    BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse
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


                var lastPageResponse =
                    await CreateLoadPaginatedItemsTask(new PaginatedRequestData(TotalPages, itemsPerPage));

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

                var responses = await Task.WhenAll(LoadItemsPages(pagesToLoad));

                for (int i = 0; i < responses.Length; i++)
                {
                    var pageNumber = (uint) ((IPaginatedResponse) responses[i].ResponseModelInterface).Paginated.Page;
                    var items = GetItemsFromResponseModelInterface(responses[i].ResponseModelInterface);

                    _paginatedData.FillPageWithItems(pageNumber,items);
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

        #region Protected methods

        #endregion

        #region Private Methods

        private async Task LoadAndStorePageItems(uint pageNumber)
        {
            try
            {
                var loadedItems = await CreateLoadPaginatedItemsTask(new PaginatedRequestData((int) pageNumber,
                    itemsPerPage));

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
           return CalculatePageNumberForGivenIndex((uint)itemIndex);
        }


        private Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.
                HttpResponse>
            LoadPageItems(uint pageNumber)
        {
            return CreateLoadPaginatedItemsTask(new PaginatedRequestData((int) pageNumber, itemsPerPage));
        }

        protected abstract
            Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.HttpResponse>
            CreateLoadPaginatedItemsTask(PaginatedRequestData paginatedRequestData);


        protected abstract List<TDataType> GetItemsFromResponseModelInterface(
            TRequestResponseModelInterface responseModelInterface);

        private Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.
                HttpResponse>[]
            LoadItemsPages(IReadOnlyList<int> pagesNumbers)
        {
            var tasks =
                new Task<BaseRequestProcessor<object, TRequestResponseDataModel, TRequestResponseModelInterface>.
                    HttpResponse>[pagesNumbers.Count];

            for (int i = 0; i < pagesNumbers.Count; i++)
            {
                tasks[i] = CreateLoadPaginatedItemsTask(new PaginatedRequestData(pagesNumbers[i], itemsPerPage));
            }

            return tasks;
        }

        private void ResetPagesProgressCounter()
        {
            _pagesProgress = 0;
        }

        private void CheckIsReachProgressStep()
        {
            if (_pagesProgress == pagesPortion)
            {
                ResetPagesProgressCounter();
            }
            else if (_pagesProgress == -pagesPortion)
            {
                ResetPagesProgressCounter();
            }
        }

        #endregion
    }
}