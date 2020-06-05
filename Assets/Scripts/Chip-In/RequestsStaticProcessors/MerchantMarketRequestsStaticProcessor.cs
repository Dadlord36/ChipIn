using System.Threading.Tasks;
using Common;
using DataModels.HttpRequestsHeadersModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class MerchantMarketRequestsStaticProcessor
    {
        public static Task<BaseRequestProcessor<object, RadarDataModel, IRadarModel>.HttpResponse> GetRadarData(
            out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            return new MerchantMarketRadarRequestsProcessor(out cancellationTokenSource, requestHeaders).SendRequest(
                "Radar data was retrieved successfully");
        }
    }
}