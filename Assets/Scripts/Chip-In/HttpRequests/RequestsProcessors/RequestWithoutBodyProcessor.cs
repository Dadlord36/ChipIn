using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using DataModels.HttpRequestsHeadersModels;

namespace HttpRequests.RequestsProcessors
{
    public abstract class RequestWithoutBodyProcessor<TResponseModel, TResponseModelInterface>
        : BaseRequestProcessor<object, TResponseModel, TResponseModelInterface>
        where TResponseModel : class, TResponseModelInterface
        where TResponseModelInterface : class
    {
        protected RequestWithoutBodyProcessor(string requestSuffix, HttpMethod requestMethod,
            IRequestHeaders requestHeaders, IReadOnlyList<string> requestParameters) :
            base(new BaseRequestProcessorParameters(requestSuffix, requestMethod,
                requestHeaders, null, requestParameters))
        {
        }

        protected RequestWithoutBodyProcessor(string requestSuffix, HttpMethod requestMethod,
            IRequestHeaders requestHeaders, IReadOnlyList<string> requestParameters,
            NameValueCollection queryStringParameters) : base(new BaseRequestProcessorParameters(
            requestSuffix, requestMethod, requestHeaders, null, requestParameters, queryStringParameters)
        )
        {
        }
    }
}