using System.Collections.Specialized;
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
        public CommunitiesListGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders,
            in string stringToSearchFor) : base(out cancellationTokenSource,
            new BaseRequestProcessorParameters(ApiCategories.Communities, HttpMethod.Get, requestHeaders, null, null,
                CreateCommunityNameParameter(stringToSearchFor)))
        {
        }

        public CommunitiesListGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders) :
            base(out cancellationTokenSource, new BaseRequestProcessorParameters(ApiCategories.Communities, HttpMethod.Get, requestHeaders,
                null, null))
        {
        }

        private static NameValueCollection CreateCommunityNameParameter(in string stringToSearchFor)
        {
            return new NameValueCollection(1) {{"name", stringToSearchFor}};
        }
    }
}