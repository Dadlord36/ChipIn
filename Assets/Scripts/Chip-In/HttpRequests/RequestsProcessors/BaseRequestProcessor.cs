using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataModels.HttpRequestsHeadersModels;
using UnityEngine;
using Utilities;
using Utilities.ApiExceptions;

namespace HttpRequests.RequestsProcessors
{
    public abstract class BaseRequestProcessor<TRequestBodyModelInterface, TResponseModel, TResponseModelInterface>
        where TResponseModel : class, TResponseModelInterface
        where TRequestBodyModelInterface : class
        where TResponseModelInterface : class
    {
        protected struct BaseRequestProcessorParameters
        {
            public readonly string RequestSuffix;
            public readonly HttpMethod RequestMethod;
            public readonly IReadOnlyList<string> RequestParameters;
            public readonly NameValueCollection QueryStringParameters;
            public readonly IRequestHeaders RequestHeaders;
            public readonly TRequestBodyModelInterface RequestBodyModel;

            public BaseRequestProcessorParameters(string requestSuffix,
                HttpMethod requestMethod, IRequestHeaders requestHeaders, TRequestBodyModelInterface requestBodyModel,
                IReadOnlyList<string> requestParameters, NameValueCollection queryStringParameters)
            {
                RequestSuffix = requestSuffix;
                RequestParameters = requestParameters;
                QueryStringParameters = queryStringParameters;
                RequestMethod = requestMethod;
                RequestHeaders = requestHeaders;
                RequestBodyModel = requestBodyModel;
            }

            public BaseRequestProcessorParameters(string requestSuffix,
                HttpMethod requestMethod, IRequestHeaders requestHeaders, TRequestBodyModelInterface requestBodyModel,
                IReadOnlyList<string> requestParameters)
            {
                RequestSuffix = requestSuffix;
                RequestParameters = requestParameters;
                QueryStringParameters = null;
                RequestMethod = requestMethod;
                RequestHeaders = requestHeaders;
                RequestBodyModel = requestBodyModel;
            }
        }

        private const string Tag = "HTTPRequests";

        private readonly BaseRequestProcessorParameters _requestProcessorParameters;
        protected bool SendBodyAsQueryStringFormat;

        protected BaseRequestProcessor(string requestSuffix, HttpMethod requestMethod, IRequestHeaders requestHeaders,
            TRequestBodyModelInterface requestBodyModel)
        {
            _requestProcessorParameters = new BaseRequestProcessorParameters(requestSuffix, requestMethod,
                requestHeaders, requestBodyModel, null, null);
        }

        protected BaseRequestProcessor(BaseRequestProcessorParameters requestProcessorParameters)
        {
            _requestProcessorParameters = requestProcessorParameters;
        }

        private static string FormUrlParametersString(IReadOnlyList<string> parameters)
        {
            if (parameters == null) return null;

            var length = parameters.Count;
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append($"/{parameters[i]}");
            }

            return stringBuilder.ToString();
        }

        private async Task<HttpResponseMessage> SendRequestToWebServer(
            TRequestBodyModelInterface requestBodyModel = null,
            IRequestHeaders requestHeaders = null)
        {
            return await ApiHelper.MakeAsyncRequest(_requestProcessorParameters.RequestMethod,
                _requestProcessorParameters.RequestSuffix,
                FormUrlParametersString(_requestProcessorParameters.RequestParameters),
                _requestProcessorParameters.QueryStringParameters,
                requestHeaders?.GetRequestHeaders(), requestBodyModel, SendBodyAsQueryStringFormat);
        }

        private static async Task<TResponseModel> ProcessResponse(HttpContent responseContent,
            bool isSuccessStatusCode, HttpStatusCode responseMessageStatusCode)
        {
            var responseAsString = await responseContent.ReadAsStringAsync();
            Debug.unityLogger.Log(LogType.Log, Tag, $"Response content: {responseAsString}");

            if (isSuccessStatusCode)
            {
                return JsonConverterUtility.ConvertJsonString<TResponseModel>(responseAsString);
            }
            else
            {
                var errorMessageBuilder = new StringBuilder();
                errorMessageBuilder.Append(responseAsString);
                errorMessageBuilder.Append("\r\n");
                errorMessageBuilder.Append($"Error Code: {responseMessageStatusCode}");
                throw new ApiException(errorMessageBuilder.ToString());
            }
        }

        public struct HttpResponse
        {
            public TResponseModelInterface ResponseModelInterface;
            public HttpHeaders Headers;
        }


        public async Task<HttpResponse> SendRequest(string successfulResponseMassage)
        {
            var httpResponse = new HttpResponse();
            using (var responseMessage = await SendRequestToWebServer(_requestProcessorParameters.RequestBodyModel,
                _requestProcessorParameters.RequestHeaders))
            {
                if (responseMessage == null)
                {
                    throw new ApiException("Response message is null");
                }

                var requestResponse =
                    await ProcessResponse(responseMessage.Content, responseMessage.IsSuccessStatusCode,
                        responseMessage.StatusCode);

                httpResponse.ResponseModelInterface = requestResponse;
                httpResponse.Headers = responseMessage.Headers;

                if (responseMessage.IsSuccessStatusCode)
                {
                    LogUtility.PrintLog(Tag, successfulResponseMassage);
                }
                else
                {
                    LogUtility.PrintLogError(Tag, responseMessage.ReasonPhrase);
                    LogUtility.PrintLogError(Tag, responseMessage.Content.ToString());
                }

                return httpResponse;
            }
        }
    }
}