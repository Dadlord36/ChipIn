using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunitiesListGetProcessor : BaseRequestProcessor<object,
        CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>
    {
        public CommunitiesListGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource,
            IRequestHeaders requestHeaders) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            ApiCategories.Communities, HttpMethod.Get, requestHeaders, null, null))
        {
        }
    }
}