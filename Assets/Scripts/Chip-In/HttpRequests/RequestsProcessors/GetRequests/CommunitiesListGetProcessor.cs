using System.Net.Http;
using System.Threading;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunitiesListGetProcessor : BaseRequestProcessor<object,
        CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>
    {
        public CommunitiesListGetProcessor(out CancellationTokenSource cancellationTokenSource,
            IRequestHeaders requestHeaders) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            ApiCategories.Communities, HttpMethod.Get, requestHeaders, null, null))
        {
        }
    }
}