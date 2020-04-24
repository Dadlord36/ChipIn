using System.Net.Http;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public sealed class CommunitiesListGetProcessor : BaseRequestProcessor<object,
        CommunityInterestLabelDataRequestResponse,
        ICommunityInterestLabelDataRequestResponse>
    {
        public CommunitiesListGetProcessor(IRequestHeaders requestHeaders) :
            base(new BaseRequestProcessorParameters(ApiCategories.Communities, HttpMethod.Get, requestHeaders,
                null, null))
        {
        }
    }
}