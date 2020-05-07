using System.Threading.Tasks;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class CommunitiesInterestsStaticProcessor
    {
        public static Task<BaseRequestProcessor<object, CommunityInterestsResponseDataModel,
                ICommunityInterestsResponseModel>.HttpResponse>
            GetCommunityOwnersInterests(IRequestHeaders requestHeaders, PaginatedRequestData paginatedRequestData)
        {
            return new CommunityOwnersInterestsPaginatedGetProcessor(requestHeaders, paginatedRequestData)
                .SendRequest("Community owners interests list was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, CommunityInterestsResponseDataModel,
            ICommunityInterestsResponseModel>.HttpResponse> GetCommunityClientsInterests(IRequestHeaders requestHeaders,
            int communityId, PaginatedRequestData paginatedRequestData)
        {
            return new CommunityClientsInterestsPaginatedGetProcessor(requestHeaders, communityId, paginatedRequestData)
                .SendRequest("Community clients interests list was retrieved successfully");
        }
    }
}