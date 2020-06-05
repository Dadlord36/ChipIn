using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.PostRequests
{
    public class CoinsTossingResult
    {
        [JsonProperty("new_coins")] public uint NewCoins { get; set; }
        [JsonProperty("tokens_balance")] public uint TokensBalance { get; set; }
    }

    public interface ITossCoinsResultModel : ISuccess
    {
        [JsonProperty("result")] CoinsTossingResult CoinsTossingResultData { get; set; }
    }

    public class TossCoinsResponseDataModel : ITossCoinsResultModel
    {
        public bool Success { get; set; }
        public CoinsTossingResult CoinsTossingResultData { get; set; }
    }

    public class TossCoinsRequestProcessor : RequestWithoutBodyProcessor<TossCoinsResponseDataModel,
        ITossCoinsResultModel>
    {
        public TossCoinsRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders) : base(
            out cancellationTokenSource, ApiCategories.Coins, HttpMethod.Post, requestHeaders, new[] {"toss"})
        {
        }
    }
}