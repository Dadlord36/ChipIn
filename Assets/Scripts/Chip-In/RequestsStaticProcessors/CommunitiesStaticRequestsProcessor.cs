using System.Threading.Tasks;
using DataModels;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Interfaces;

namespace RequestsStaticProcessors
{
    public static class CommunitiesStaticRequestsProcessor
    {
        public static Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse,
                ICommunitiesBasicDataRequestResponse>.HttpResponse>
            GetCommunitiesList(IRequestHeaders requestHeaders)
        {
            return new CommunitiesListGetProcessor(requestHeaders)
                .SendRequest("Communities data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>.HttpResponse> GetPaginatedCommunitiesList(IRequestHeaders requestHeaders,
            PaginationData paginationData)
        {
            return new CommunitiesPaginatedListGetProcessor(requestHeaders, paginationData)
                .SendRequest("Communities paginated data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunityItemResponseDataModel, ICommunityItemResponseModel>.HttpResponse>
            GetCommunityDetails(IRequestHeaders requestHeaders, int communityId)
        {
            return new CommunityDetailsGetProcessor(requestHeaders, communityId).SendRequest("Community was retrieved");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            JoinCommunity(IRequestHeaders requestHeaders, int communityId)
        {
            return new CommunityJoinPostProcessor(requestHeaders, communityId)
                .SendRequest($"Joined successfully to community {communityId.ToString()}");
        }

        public static Task<BaseRequestProcessor<object, SuccessConfirmationModel, ISuccess>.HttpResponse>
            LeaveCommunity(IRequestHeaders requestHeaders, int communityId)
        {
            return new CommunityLeaveDeleteProcessor(requestHeaders, communityId)
                .SendRequest($"Leaving successfully community {communityId.ToString()}");
        }
    }
}