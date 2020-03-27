using System.Threading.Tasks;
using DataModels.HttpRequestsHeadersModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class MerchantMarketRequestsStaticProcessor
    {
        public static Task<BaseRequestProcessor<object, RadarDataModel, IRadarModel>.HttpResponse> GetRadarData(IRequestHeaders requestHeaders)
        {
            return new MerchantMarketRadarRequestsProcessor(requestHeaders).SendRequest(
                "Radar data was retrieved successfully");
        }
    }
}