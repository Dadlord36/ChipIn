using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class
        DetailedOfferGetProcessor : BaseRequestProcessor<object, OfferDetailsResponseModel, IOfferDetailsResponseModel>
    {
        public class DetailedOfferGetProcessorParameters
        {
            public readonly IRequestHeaders RequestHeaders;
            public readonly int? OfferId;

            public DetailedOfferGetProcessorParameters(IRequestHeaders requestHeaders, int? offerId)
            {
                RequestHeaders = requestHeaders;
                OfferId = offerId;
            }
        }

        public DetailedOfferGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource,
            DetailedOfferGetProcessorParameters parameters) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            ApiCategories.Offers, HttpMethod.Get, parameters.RequestHeaders, null, new[] {parameters.OfferId.ToString()}))
        {
        }
    }
}