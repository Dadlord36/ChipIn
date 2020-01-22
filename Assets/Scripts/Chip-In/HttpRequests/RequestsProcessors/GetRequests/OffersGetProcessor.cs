using System.Net.Http;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class OffersGetProcessor : BaseRequestProcessor<object, OffersResponseModel, IOffersResponseModel>
    {
        public OffersGetProcessor(IRequestHeaders requestHeaders) : base(RequestsSuffixes.Offers, HttpMethod.Get,
            requestHeaders, null)
        {
        }
    }
}