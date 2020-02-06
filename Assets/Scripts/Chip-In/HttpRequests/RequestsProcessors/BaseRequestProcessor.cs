using System;
using System.Collections.Generic;
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
            public readonly IReadOnlyList<string> RequestParameters;
            public readonly HttpMethod RequestMethod;
            public readonly IRequestHeaders RequestHeaders;
            public readonly TRequestBodyModelInterface RequestBodyModel;

            public BaseRequestProcessorParameters(string requestSuffix,
                HttpMethod requestMethod, IRequestHeaders requestHeaders, TRequestBodyModelInterface requestBodyModel,
                IReadOnlyList<string> requestParameters)
            {
                RequestSuffix = requestSuffix;
                RequestParameters = requestParameters;
                RequestMethod = requestMethod;
                RequestHeaders = requestHeaders;
                RequestBodyModel = requestBodyModel;
            }
        }

        private const string Tag = "HTTPRequests";

        private readonly string _requestSuffix;
        private readonly string _requestParameters;
        private readonly HttpMethod _requestMethod;
        private readonly IRequestHeaders _requestHeaders;
        private readonly TRequestBodyModelInterface _requestBodyModel;


        protected BaseRequestProcessor(string requestSuffix, HttpMethod requestMethod, IRequestHeaders requestHeaders,
            TRequestBodyModelInterface requestBodyModel)
        {
            _requestSuffix = requestSuffix;
            _requestParameters = "";
            _requestMethod = requestMethod;
            _requestHeaders = requestHeaders;
            _requestBodyModel = requestBodyModel;
        }

        protected BaseRequestProcessor(BaseRequestProcessorParameters requestProcessorParameters)
        {
            _requestSuffix = requestProcessorParameters.RequestSuffix;
            _requestParameters = requestProcessorParameters.RequestParameters == null
                ? null
                : FormUrlParametersString(requestProcessorParameters.RequestParameters);
            _requestMethod = requestProcessorParameters.RequestMethod;
            _requestHeaders = requestProcessorParameters.RequestHeaders;
            _requestBodyModel = requestProcessorParameters.RequestBodyModel;
        }

        private static string FormUrlParametersString(IReadOnlyList<string> parameters)
        {
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
            return await ApiHelper.MakeAsyncRequest(_requestMethod, _requestSuffix, _requestParameters,
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
            using (var responseMessage = await SendRequestToWebServer(_requestBodyModel, _requestHeaders))
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

                    Debug.unityLogger.Log(LogType.Log, Tag, requestResponse.ResponseMessage.IsSuccessStatusCode
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