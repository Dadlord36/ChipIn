using System.Net.Http;
using System.Threading;
using DataModels.HttpRequestsHeadersModels;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class OffersGetProcessor : BaseRequestProcessor<object, OffersResponseModel, IOffersResponseModel>
    {
        public OffersGetProcessor(out CancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders) : base(
            out cancellationTokenSource, ApiCategories.Offers, HttpMethod.Get, requestHeaders, null)
        {
        }
    }
}