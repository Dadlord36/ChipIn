using System.Net.Http;
using DataModels.Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunitiesPaginatedListGetProcessor : BaseRequestProcessor<object, CommunitiesBasicDataRequestResponse,
        ICommunitiesBasicDataRequestResponse>
    {
        public CommunitiesPaginatedListGetProcessor(IRequestHeaders requestHeaders, PaginationData pagination) :
            base(new BaseRequestProcessorParameters(ApiCategories.Communities, HttpMethod.Get, requestHeaders,
                null, null, pagination.ConvertPaginationToNameValueCollection()))
        {
        }
    }
}