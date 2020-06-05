using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;

namespace HttpRequests.RequestsProcessors
{
    public abstract class RequestWithoutBodyProcessor<TResponseModel, TResponseModelInterface>
        : BaseRequestProcessor<object, TResponseModel, TResponseModelInterface>
        where TResponseModel : class, TResponseModelInterface
        where TResponseModelInterface : class
    {
        protected RequestWithoutBodyProcessor(out DisposableCancellationTokenSource cancellationTokenSource, string requestSuffix,
            HttpMethod requestMethod, IRequestHeaders requestHeaders, IReadOnlyList<string> requestParameters) :
            base(out cancellationTokenSource, new BaseRequestProcessorParameters(requestSuffix, requestMethod,
                requestHeaders, null, requestParameters))
        {
        }

        protected RequestWithoutBodyProcessor(out DisposableCancellationTokenSource cancellationTokenSource, string requestSuffix,
            HttpMethod requestMethod, IRequestHeaders requestHeaders, IReadOnlyList<string> requestParameters,
            NameValueCollection queryStringParameters) : base(out cancellationTokenSource, new BaseRequestProcessorParameters(
            requestSuffix, requestMethod, requestHeaders, null, requestParameters, queryStringParameters))
        {
        }
    }
}