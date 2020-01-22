using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class
        DetailedOfferGetProcessor : BaseRequestProcessor<object, OfferDetailedModel, IOfferDetailedModel>
    {
        public struct DetailedOfferGetProcessorParameters
        {
            public readonly IRequestHeaders RequestHeaders;
            public readonly int OfferId;

            public DetailedOfferGetProcessorParameters(IRequestHeaders requestHeaders, int offerId)
            {
                RequestHeaders = requestHeaders;
                OfferId = offerId;
            }
        }

        public DetailedOfferGetProcessor(DetailedOfferGetProcessorParameters parameters) : base(
            new BaseRequestProcessorParameters(RequestsSuffixes.Offers, new[] {parameters.OfferId.ToString()},
                HttpMethod.Get, parameters.RequestHeaders, null))
        {
        }
    }
}