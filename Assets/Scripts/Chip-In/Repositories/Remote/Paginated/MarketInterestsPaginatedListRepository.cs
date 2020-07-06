using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using RequestsStaticProcessors;
using UnityEngine;

namespace Repositories.Remote.Paginated {
    [CreateAssetMenu (fileName = nameof (MarketInterestsPaginatedListRepository),
        menuName = nameof (Repositories) + "/" + nameof (Remote) + "/" + nameof (MarketInterestsPaginatedListRepository), order = 0)]
    public class MarketInterestsPaginatedListRepository : PaginatedItemsListRepository<MarketInterestDetailsDataModel, MarketInterestsDetailsDataRequestResponse, IMarketInterestsDetailsDataRequestResponse> {
            protected override string Tag => nameof (MarketInterestsPaginatedListRepository);

            protected override Task<BaseRequestProcessor<object, MarketInterestsDetailsDataRequestResponse, IMarketInterestsDetailsDataRequestResponse>.HttpResponse> CreateLoadPaginatedItemsTask (out DisposableCancellationTokenSource cancellationTokenSource, PaginatedRequestData paginatedRequestData) {
                var communitiesListRequestTask = CommunitiesStaticRequestsProcessor.GetPaginatedCommunitiesList (out cancellationTokenSource,
                    authorisationDataRepository, paginatedRequestData);

                var transitCancellationToken = cancellationTokenSource;

                return communitiesListRequestTask.ContinueWith (async (Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse> task) => {
                    int?[] CollectCommunitiesIdentifiers (IReadOnlyList<InterestBasicDataModel> dataModels) {
                        var identifiers = new int?[dataModels.Count];

                        for (int i = 0; i < dataModels.Count; i++) {
                            identifiers[i] = dataModels[i].Id;
                        }

                        return identifiers;
                    }

                    var communitiesIdentifiers = CollectCommunitiesIdentifiers (task.Result.ResponseModelInterface.Communities);
                    var pagination = task.Result.ResponseModelInterface.Paginated;

                    var result = CreateInterestsDetailsRequestsTask (communitiesIdentifiers, out var innerCancellationTokenSource);
                    transitCancellationToken.Token.Register (innerCancellationTokenSource.Cancel);

                    var interestsDetails = await result;
                    var marketInterestDetailsDataModels = new MarketInterestDetailsDataModel[interestsDetails.Length];

                    for (int i = 0; i < interestsDetails.Length; i++) {
                        marketInterestDetailsDataModels[i] = interestsDetails[i].ResponseModelInterface.LabelDetailsDataModel;
                    }

                    return new BaseRequestProcessor<object, MarketInterestsDetailsDataRequestResponse, IMarketInterestsDetailsDataRequestResponse>.
                    HttpResponse {
                        Success = true,
                            ResponseModelInterface = new MarketInterestsDetailsDataRequestResponse {
                                Success=true,
                                MarketInterestsDetails = marketInterestDetailsDataModels,
                                Paginated = pagination
                                }
                    };
                }, TaskContinuationOptions.OnlyOnRanToCompletion).Unwrap ();
            }

            private Task<BaseRequestProcessor<object, InterestDetailsResponseDataModel, IInterestDetailsResponseModel>.HttpResponse[]>
            CreateInterestsDetailsRequestsTask (int?[] communitiesIdentifiers, out DisposableCancellationTokenSource cancellationTokenSource) {
                cancellationTokenSource = null;
                var tasks = new List<Task<BaseRequestProcessor<object, InterestDetailsResponseDataModel, IInterestDetailsResponseModel>.HttpResponse>>
                    (communitiesIdentifiers.Length);

                for (int i = 0; i < communitiesIdentifiers.Length; i++) {
                    tasks.Add (CommunitiesStaticRequestsProcessor.GetCommunityDetails (out cancellationTokenSource, authorisationDataRepository,
                        (int) communitiesIdentifiers[i]));
                }

                return Task.WhenAll (tasks);
            }

            protected override List<MarketInterestDetailsDataModel> GetItemsFromResponseModelInterface (IMarketInterestsDetailsDataRequestResponse responseModelInterface) {
                return new List<MarketInterestDetailsDataModel> (responseModelInterface.MarketInterestsDetails);
            }

            public override Task SaveDataToServer () {
                throw new NotImplementedException ();
            }
        }
}