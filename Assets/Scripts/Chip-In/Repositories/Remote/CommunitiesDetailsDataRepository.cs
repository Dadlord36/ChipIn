using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataModels;
using DataModels.Common;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(CommunitiesDetailsDataRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(CommunitiesDetailsDataRepository), order = 0)]
    public sealed class CommunitiesDetailsDataRepository : BaseItemsListRepository<CommunityDetailsDataModel>
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;


        public override async Task LoadDataFromServer()
        {
            try
            {
                var result = await CommunitiesStaticRequestsProcessor.GetCommunitiesList(authorisationDataRepository);
                var responseInterface = result.ResponseModelInterface;
                var items = await LoadCommunitiesDetailsData(responseInterface.Communities);
                ItemsLiveData = new PaginatedList<CommunityDetailsDataModel>(responseInterface.Pagination, items);
                ConfirmDataLoading();
            }

            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
            }
        }

        private async Task<CommunityDetailsDataModel[]> LoadCommunitiesDetailsData(
            IReadOnlyList<CommunityBasicDataModel> communitiesBasicData)
        {
            var count = communitiesBasicData.Count;

            var tasks = new Task<BaseRequestProcessor<object, CommunityItemResponseDataModel,
                ICommunityItemResponseModel>.HttpResponse>[count];

            for (int i = 0; i < count; i++)
            {
                var id = (int) communitiesBasicData[i].Id;
                tasks[i] = CommunitiesStaticRequestsProcessor.GetCommunityDetails(authorisationDataRepository, id);
            }


            var result = await Task.WhenAll(tasks);
            var dataModels = new CommunityDetailsDataModel[count];

            for (var index = 0; index < result.Length; index++)
            {
                dataModels[index] = result[index].ResponseModelInterface.LabelDetailsDataModel;
            }

            return dataModels;
        }

        public override async Task SaveDataToServer()
        {
            throw new NotImplementedException();
        }
    }
}