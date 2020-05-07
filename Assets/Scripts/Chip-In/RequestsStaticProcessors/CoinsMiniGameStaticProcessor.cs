using System.Threading.Tasks;
using DataModels.HttpRequestsHeadersModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.PostRequests;

namespace RequestsStaticProcessors
{
    public static class CoinsMiniGameStaticProcessor
    {
        private const string Tag = nameof(CoinsMiniGameStaticProcessor);

        public static Task<BaseRequestProcessor<object, TossCoinsResponseDataModel, ITossCoinsResultModel>.HttpResponse>
            TossACoin(IRequestHeaders requestHeaders)
        {
            return new TossCoinsRequestProcessor(requestHeaders)
                .SendRequest("Coins was tossed successfully");
        }
    }
}