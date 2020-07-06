using System.Net.Http;
using Common;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public interface IInterestDetailsResponseModel : ISuccess
    {
        [JsonProperty("community")] MarketInterestDetailsDataModel LabelDetailsDataModel { get; set; }
    }

    public sealed class CommunityDetailsGetProcessor : RequestWithoutBodyProcessor<InterestDetailsResponseDataModel,
        IInterestDetailsResponseModel>
    {
        public CommunityDetailsGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            int communityId) : base(out cancellationTokenSource, ApiCategories.Communities, HttpMethod.Get, requestHeaders,
            new[] {communityId.ToString()})
        {
        }
    }
}