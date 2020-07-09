using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class OffersGetProcessor : BaseRequestProcessor<object, OffersResponseModel, IOffersResponseModel>
    {
        public OffersGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders) : base(
            out cancellationTokenSource, ApiCategories.Offers, HttpMethod.Get, requestHeaders, null)
        {
        }
    }
}