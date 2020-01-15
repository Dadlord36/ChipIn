using System;
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
        private const string Tag = "HTTPRequests"; 
        
        private readonly string _requestSuffix;
        private readonly HttpMethod _requestMethod;
        private readonly IRequestHeaders _requestHeaders;
        private readonly TRequestBodyModelInterface _requestBodyModel;
        
        protected BaseRequestProcessor(string requestSuffix, HttpMethod requestMethod,
            IRequestHeaders requestHeaders, TRequestBodyModelInterface requestBodyModel)
        {
            _requestSuffix = requestSuffix;
            _requestMethod = requestMethod;
            _requestHeaders = requestHeaders;
            _requestBodyModel = requestBodyModel;
        }

        private async Task<HttpResponseMessage> SendRequestToWebServer(
            TRequestBodyModelInterface requestBodyModel = null,
            IRequestHeaders requestHeaders = null)
        {
            return await ApiHelper.MakeAsyncRequest(_requestMethod, _requestSuffix,
                requestHeaders?.GetRequestHeaders(), requestBodyModel);
        }

        private static async Task<RequestResponse<TResponseModelInterface>> ProcessResponse(
            HttpResponseMessage responseMessage)
        {
            if (responseMessage == null)
            {
                throw new Exception("Response message is null");
            }

            if (responseMessage.IsSuccessStatusCode)
            {
                var responseData =
                    await JsonConverterUtility.ContentAsyncJsonTo<TResponseModel>(responseMessage.Content);
                return new RequestResponse<TResponseModelInterface>(responseMessage, responseData);
            }

            {
                var responseAsString = await responseMessage.Content.ReadAsStringAsync();
                Debug.unityLogger.Log(LogType.Log, Tag,$"Response message: {responseAsString}");
                
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
                       throw new Exception ("Response data is equals null");
                    }
                    else
                    {
                        httpResponse.ResponseModelInterface = requestResponse.ResponseData;
                        httpResponse.Headers = requestResponse.ResponseMessage.Headers;
                    }

                    Debug.unityLogger.Log(LogType.Log, Tag,requestResponse.ResponseMessage.IsSuccessStatusCode
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