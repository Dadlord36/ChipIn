using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;

namespace Repositories
{
    [CreateAssetMenu(menuName = nameof(Repositories) + "/" + nameof(Remote) + nameof(CommunitiesBaseDataPaginatedListRepository),
        fileName = "Create " + nameof(CommunitiesBaseDataPaginatedListRepository), order = 0)]
    public class CommunitiesBaseDataPaginatedListRepository : BasePaginatedItemsListRepository<CommunityBasicDataModel>
    {
        [Space(25f)] [SerializeField] private int itemsPerPage;
        [SerializeField] private byte maxCachedPagesCount;
        [SerializeField] private byte pagesPortion;
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        private short _pagesProgress;
        private uint CurrentPage { get; set; } = 1;


        public Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            TryLoadItemsPages(uint pageNumber)
        {
            return LoadPaginatedItems(new PaginatedRequestData((int) pageNumber, itemsPerPage));
        }

        private void OnEnable()
        {
            PaginatedData = new PaginatedList<CommunityBasicDataModel>(maxCachedPagesCount, itemsPerPage);
        }

        public bool TryGetNextListPage(out List<CommunityBasicDataModel> items)
        {
            if (PaginatedData.TryGetNextPageItems(out items))
            {
                return true;
            }
            else
            {
                AddNextItemsPortion();
                return PaginatedData.TryGetNextPageItems(out items);
            }
        }

        public bool TryGetPreviousListPage(out List<CommunityBasicDataModel> items)
        {
            return PaginatedData.TryGetPreviousPageItems(out items);
        }

        public bool TryGetCurrentPageItems(out List<CommunityBasicDataModel> items)
        {
            return PaginatedData.TryGetCurrentPageItems(out items);
        }

        private Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            LoadPageItems(uint pageNumber)
        {
            return LoadPaginatedItems(new PaginatedRequestData((int) pageNumber, itemsPerPage));
        }

        private Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            LoadPaginatedItems(PaginatedRequestData paginatedRequestData)
        {
            return CommunitiesStaticRequestsProcessor.GetPaginatedCommunitiesList(authorisationDataRepository, paginatedRequestData);
        }

        private Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>[]
            LoadItemsPages(IReadOnlyList<int> pagesNumbers)
        {
            var tasks = new Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse,
                ICommunitiesBasicDataRequestResponse>.HttpResponse>[pagesNumbers.Count];

            for (int i = 0; i < pagesNumbers.Count; i++)
            {
                tasks[i] = LoadPaginatedItems(new PaginatedRequestData(pagesNumbers[i], itemsPerPage));
            }

            return tasks;
        }

        private async void AddNextItemsPortion()
        {
            try
            {
                var loadedItems = await LoadPaginatedItems(new PaginatedRequestData((int) (CurrentPage + 1),
                    itemsPerPage));

                if (!loadedItems.Success || loadedItems.ResponseModelInterface.Communities.Length == 0) return;

                CurrentPage++;
                PaginatedData.FillPageWithItems(CurrentPage, loadedItems.ResponseModelInterface.Communities);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
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

        public override async Task LoadDataFromServer()
        {
            try
            {
                const int initialPage = 1;
                var response = await LoadPaginatedItems(new PaginatedRequestData(initialPage, itemsPerPage));
                var responseModel = response.ResponseModelInterface;
                if (responseModel.PaginatedResponse.Total <= 1)
                {
                    return;
                }

                PaginatedData.FillPageWithItems(initialPage, responseModel.Communities);

                var totalPages = responseModel.PaginatedResponse.Total;
                if (totalPages - 1 < 1) return;

                var pagesToLoad = new int[totalPages - 1];

                for (int i = 0; i < pagesToLoad.Length; i++)
                {
                    pagesToLoad[i] = initialPage + i + 1;
                }

                var responses = await Task.WhenAll(LoadItemsPages(pagesToLoad));

                for (int i = 0; i < responses.Length; i++)
                {
                    PaginatedData.FillPageWithItems((uint) responses[i].ResponseModelInterface.PaginatedResponse.Page,
                        responses[i].ResponseModelInterface.Communities);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public override async Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}