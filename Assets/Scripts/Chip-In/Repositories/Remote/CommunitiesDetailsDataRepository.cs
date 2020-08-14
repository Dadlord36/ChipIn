using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
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
    public sealed class CommunitiesDetailsDataRepository : BaseNotPaginatedListRepository<MarketInterestDetailsDataModel>
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;


        public override async Task LoadDataFromServer()
        {
            try
            {
                var result = await CommunitiesStaticRequestsProcessor.GetCommunitiesList(out TasksCancellationTokenSource,
                    authorisationDataRepository);
                var responseInterface = result.ResponseModelInterface;
                var items = await LoadCommunitiesDetailsData(responseInterface.Communities).ConfigureAwait(false);
                ItemsLiveData = new LiveData<MarketInterestDetailsDataModel>(items);
                ConfirmDataLoading();
            }

            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private async Task<MarketInterestDetailsDataModel[]> LoadCommunitiesDetailsData(IReadOnlyList<InterestBasicDataModel> communitiesBasicData)
        {
            var count = communitiesBasicData.Count;

            var tasks = new Task<BaseRequestProcessor<object, InterestDetailsResponseDataModel, IInterestDetailsResponseModel>.HttpResponse>[count];

            for (int i = 0; i < count; i++)
            {
                var id = (int) communitiesBasicData[i].Id;
                tasks[i] = CommunitiesStaticRequestsProcessor.GetCommunityDetails(out TasksCancellationTokenSource, authorisationDataRepository, id);
            }
            
            try
            {
                var result = await Task.WhenAll(tasks).ConfigureAwait(false);
                var dataModels = new MarketInterestDetailsDataModel[count];

                for (var index = 0; index < result.Length; index++)
                {
                    dataModels[index] = result[index].ResponseModelInterface.LabelDetailsDataModel;
                }

                return dataModels;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}