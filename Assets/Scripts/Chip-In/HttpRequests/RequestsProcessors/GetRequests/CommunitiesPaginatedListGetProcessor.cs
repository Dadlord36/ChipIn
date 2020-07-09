using System.Net.Http;
using System.Threading;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunitiesPaginatedListGetProcessor : BaseRequestProcessor<object,
        CommunitiesBasicDataRequestResponse,
        ICommunitiesBasicDataRequestResponse>
    {
        public CommunitiesPaginatedListGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            ApiCategories.Communities, HttpMethod.Get, requestHeaders, null, null,
            paginatedRequestData.ConvertPaginationToNameValueCollection()))
        {
        }
    }
}