using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using DataModels.HttpRequestsHeadersModels;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Utilities;

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
            LogUtility.PrintLog(Tag, $"Response content: {responseAsString}");

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
                LogUtility.PrintLog(Tag, errorMessageBuilder.ToString());
            }

            return default;
        }

        public struct HttpResponse : ISuccess
        {
            public TResponseModelInterface ResponseModelInterface;
            public HttpHeaders Headers;
            public string ResponsePhrase;
            public string Error;
            public bool Success { get; set; }
        }


        public async Task<HttpResponse> SendRequest(string successfulResponseMassage)
        {
            using (var responseMessage = await SendRequestToWebServer(_requestProcessorParameters.RequestBodyModel,
                _requestProcessorParameters.RequestHeaders))
            {
                if (responseMessage == null)
                {
                    LogUtility.PrintLogError(Tag, "Response message is null");
                    return default;
                }

                var requestResponse = await ProcessResponse(responseMessage.Content, responseMessage.IsSuccessStatusCode, responseMessage.StatusCode);
                var httpResponse = new HttpResponse
                {
                    ResponseModelInterface = requestResponse, Headers = responseMessage.Headers, ResponsePhrase = responseMessage.ReasonPhrase,
                    Success = responseMessage.IsSuccessStatusCode
                };

                if (responseMessage.IsSuccessStatusCode)
                {
                    LogUtility.PrintLog(Tag, successfulResponseMassage);
                }
                else
                {
                    var contentAsString = await responseMessage.Content.ReadAsStringAsync();
                    httpResponse.Error = CollectErrors(contentAsString);

                    LogUtility.PrintLogError(Tag, $"ResponsePhrase: {responseMessage.ReasonPhrase}");
                    LogUtility.PrintLog(Tag, $"Content string: {contentAsString}");
                }

                return httpResponse;
            }
        }

        private struct ApiSimpleErrors
        {
            [JsonProperty("errors")] public string[] ErrorsArray;
        }

        private struct ApiStructuredErrors
        {
            public struct ErrorModel
            {
                [JsonProperty("key")] public string Key;
                [JsonProperty("messages")] public string Message;
            }

            [JsonProperty("errors")] public ErrorModel[] ErrorData;
        }

        private struct ApiDeepStructuredErrors
        {
            public struct DeepStructuredError
            {
                [JsonProperty("key")] public string Key;
                [JsonProperty("messages")] public string[] Messages;
            }

            [JsonProperty("errors")] public DeepStructuredError[] ErrorData;
        }

        private static string CollectErrors(string contentAsString)
        {
            if (JsonConverterUtility.TryParseJsonEveryExistingMember<ApiDeepStructuredErrors>(contentAsString, out var deepStructuredErrors))
            {
                return BuildErrorStringFromDeepStructuredErrors(deepStructuredErrors.ErrorData);
            }

            if (JsonConverterUtility.TryParseJsonEveryExistingMember<ApiSimpleErrors>(contentAsString, out var simpleErrorsData))
            {
                return BuildErrorString(simpleErrorsData.ErrorsArray);
            }

            if (JsonConverterUtility.TryParseJsonEveryExistingMember<ApiStructuredErrors>(contentAsString, out var structuredErrorsData))
            {
                return BuildErrorStringFromStructuredErrors(structuredErrorsData.ErrorData);
            }


            string BuildErrorString(string[] errors)
            {
                var stringBuilder = new StringBuilder(errors[0]);
                for (int i = 1; i < errors.Length; i++)
                {
                    stringBuilder.Append($" {errors[i]}");
                }

                return stringBuilder.ToString();
            }

            string BuildErrorStringFromStructuredErrors(ApiStructuredErrors.ErrorModel[] errors)
            {
                var stringBuilder = new StringBuilder(BuildElement(errors[0]));
                for (int i = 1; i < errors.Length; i++)
                {
                    stringBuilder.Append(BuildElement(errors[i]));
                }

                string BuildElement(ApiStructuredErrors.ErrorModel errorElementData)
                {
                    return $"{errorElementData.Key} {errorElementData.Message}";
                }

                return stringBuilder.ToString();
            }

            string BuildErrorStringFromDeepStructuredErrors(ApiDeepStructuredErrors.DeepStructuredError[] errors)
            {
                var stringBuilder = new StringBuilder(BuildElement(errors[0]));
                for (int i = 1; i < errors.Length; i++)
                {
                    stringBuilder.Append(BuildElement(errors[i]));
                }

                string BuildElement(ApiDeepStructuredErrors.DeepStructuredError errorElementData)
                {
                    return $"{errorElementData.Key} {BuildMessageString(errorElementData.Messages)}";

                    string BuildMessageString(string[] messageStrings)
                    {
                        var messageStringBuilder = new StringBuilder(messageStrings[0]);
                        for (int i = 1; i < messageStrings.Length; i++)
                        {
                            messageStringBuilder.Append($" {messageStrings[i]}");
                        }

                        return messageStringBuilder.ToString();
                    }
                }

                return stringBuilder.ToString();
            }

            LogUtility.PrintLogError(Tag, "Given content has no errors");
            return null;
        }
    }
}