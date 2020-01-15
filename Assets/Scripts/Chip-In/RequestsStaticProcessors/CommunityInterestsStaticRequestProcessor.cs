using System.Threading.Tasks;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class CommunityInterestsStaticRequestProcessor
    {
        public static async
            Task<BaseRequestProcessor<object, CommunityInterestLabelDataRequestResponse,
                ICommunityInterestLabelDataRequestResponse>.HttpResponse> GetCommunityInterestsLabelsData(
                IRequestHeaders requestHeaders)
        {
            return await new CommunityInterestsLabelDataGetProcessor(requestHeaders).SendRequest(
                "CommunityInterestsLabelData was retrieved successfully");
        }
    }
}