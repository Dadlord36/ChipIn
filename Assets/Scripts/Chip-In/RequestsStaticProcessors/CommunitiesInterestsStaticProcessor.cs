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
    }
}