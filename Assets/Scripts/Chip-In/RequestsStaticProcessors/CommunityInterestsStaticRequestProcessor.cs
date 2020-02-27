using System;
using System.Threading.Tasks;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class CommunityInterestsStaticRequestProcessor
    {
        public static async
            Task<BaseRequestProcessor<object, CommunityInterestLabelDataRequestResponse, ICommunityInterestLabelDataRequestResponse>.HttpResponse> 
            TryGetCommunityInterestsLabelsData(IRequestHeaders requestHeaders)
        {
            try
            {
                return await new CommunityInterestsLabelDataGetProcessor(requestHeaders).SendRequest("CommunityInterestsLabelData was retrieved successfully");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}