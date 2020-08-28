using System.Threading.Tasks;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Remote;

namespace RequestsStaticProcessors
{
    public static class CommunitiesStaticRequestsProcessor
    {
        public static Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            GetCommunitiesListByName(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, in string searchForString)
        {
            return new CommunitiesListGetProcessor(out cancellationTokenSource, requestHeaders, searchForString)
                .SendRequest("Communities data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            GetCommunitiesList(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            return new CommunitiesListGetProcessor(out cancellationTokenSource, requestHeaders)
                .SendRequest("Communities data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse>
            GetUserCommunitiesPaginatedList(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData)
        {
            return new UsersCommunitiesPaginatedListGetProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData)
                .SendRequest("User Communities paginated data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, MerchantInterestsResponseDataModel, IMerchantInterestsResponseModel>.HttpResponse>
            GetOwnersCommunityPaginatedList(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
                PaginatedRequestData paginatedRequestData)
        {
            return new OwnerCommunitiesPaginatedListGetProcessor(out cancellationTokenSource, requestHeaders, paginatedRequestData)
                .SendRequest("Owner Communities interests paginated data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, InterestDetailsResponseDataModel, IInterestDetailsResponseModel>.HttpResponse>
            GetCommunityDetails(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int communityId)
        {
            return new CommunityDetailsGetProcessor(out cancellationTokenSource, requestHeaders, communityId)
                .SendRequest("Community details data was retrieved");
        }
    }
}