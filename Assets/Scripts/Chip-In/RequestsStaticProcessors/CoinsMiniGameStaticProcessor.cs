using System.Threading.Tasks;
using Common;
using DataModels.HttpRequestsHeadersModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.PostRequests;

namespace RequestsStaticProcessors
{
    public static class CoinsMiniGameStaticProcessor
    {
        private const string Tag = nameof(CoinsMiniGameStaticProcessor);

        public static Task<BaseRequestProcessor<object, TossCoinsResponseDataModel, ITossCoinsResultModel>.HttpResponse>
            TossACoin(out DisposableCancellationTokenSource cancellationTokensSource, IRequestHeaders requestHeaders)
        {
            return new TossCoinsRequestProcessor(out cancellationTokensSource, requestHeaders)
                .SendRequest("Coins was tossed successfully");
        }
    }
}