using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        private BaseRequestProcessorParameters _requestProcessorParameters;

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
                requestHeaders?.GetRequestHeaders(), requestBodyModel);
        }

        private static async Task<RequestResponse<TResponseModelInterface>> ProcessResponse(
            HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
            {
                throw new Exception("Response message is null");
            }

            var responseAsString = await responseMessage.Content.ReadAsStringAsync();
            Debug.unityLogger.Log(LogType.Log, Tag, $"Response content: {responseAsString}");

            if (responseMessage.IsSuccessStatusCode)
            {
                return new RequestResponse<TResponseModelInterface>(responseMessage,
                    JsonConverterUtility.ContentAsyncJsonTo<TResponseModel>(responseAsString));
            }

            {
                var errorMessageBuilder = new StringBuilder();
                try
                {
                    errorMessageBuilder.Append(responseAsString);
                }
                catch (ApiException _)
                {
                }

                errorMessageBuilder.Append("\r\n");
                errorMessageBuilder.Append($"Error Code: {responseMessage.StatusCode}");
                responseMessage.Dispose();
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
                try
                {
                    var requestResponse = await ProcessResponse(responseMessage);
                    if (requestResponse.ResponseData == null)
                    {
                        throw new Exception("Response data is equals null");
                    }
                    else
                    {
                        httpResponse.ResponseModelInterface = requestResponse.ResponseData;
                        httpResponse.Headers = requestResponse.ResponseMessage.Headers;
                    }

                    LogUtility.PrintLog(Tag, requestResponse.ResponseMessage.IsSuccessStatusCode
                        ? successfulResponseMassage
                        : requestResponse.ResponseMessage.ReasonPhrase);
                }
                catch (Exception e)
                {
                    Debug.unityLogger.LogException(e);
                }

                return httpResponse;
            }
        }
    }
}