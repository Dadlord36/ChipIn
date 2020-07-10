using System.Net.Http;
using Common;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class UsersCommunitiesPaginatedListGetProcessor : BaseRequestProcessor<object,
        CommunitiesBasicDataRequestResponse, ICommunitiesBasicDataRequestResponse>
    {
        public UsersCommunitiesPaginatedListGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            PaginatedRequestData paginatedRequestData) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            ApiCategories.Communities, HttpMethod.Get, requestHeaders, null, null,
            paginatedRequestData.ConvertPaginationToNameValueCollection()))
        {
        }
    }
}