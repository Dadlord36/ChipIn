using System.Threading.Tasks;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class CommunitiesStaticRequestsProcessor
    {
        public static Task<BaseRequestProcessor<object, CommunityInterestLabelDataRequestResponse,
                ICommunityInterestLabelDataRequestResponse>.HttpResponse>
            GetCommunitiesList(IRequestHeaders requestHeaders)
        {
            return new CommunitiesListGetProcessor(requestHeaders)
                .SendRequest("Communities data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunityInterestLabelDataRequestResponse, ICommunityInterestLabelDataRequestResponse>.HttpResponse> GetPaginatedCommunitiesList(IRequestHeaders requestHeaders,
            PaginationData paginationData)
        {
            return new CommunitiesPaginatedListGetProcessor(requestHeaders, paginationData)
                .SendRequest("Communities paginated data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunityItemResponseDataModel, ICommunityItemResponseModel>.HttpResponse>
            GetCommunityById(IRequestHeaders requestHeaders, int communityId)
        {
            return new CommunitiesByIdGetProcessor(requestHeaders, communityId).SendRequest("Community was retrieved");
        }

        public static void JoinCommunity()
        {
            
        }

        public static void LeaveCommunity()
        {
            
        }
    }
}