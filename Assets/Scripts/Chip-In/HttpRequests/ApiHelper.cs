using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Controllers;
using Factories;
using RestSharp;
using UnityEngine.Assertions;
using Utilities;
using WebOperationUtilities;

namespace HttpRequests
{
    public static class ApiHelper
    {
        private const string Tag = nameof(ApiHelper);

        private static HttpClient _mainApiClient;

        public const string JsonMediaTypeHeader = "application/json";
        public const string MultipartFormData = "multipart/form-data";

        private const string ApiUri = "http://chip-in-dev.herokuapp.com/", ApiVersion = "api/v1/";

        public static string ApiUrl => ApiUri + ApiVersion;
        public static HttpClient DefaultClient { get; private set; }
        public static RestClient DefaultRestClient { get; private set; }

        public static void InitializeClient()
        {
            if (DefaultClient == null)
            {
                DefaultClient = new HttpClient();
            }

            if (DefaultRestClient == null)
            {
                DefaultRestClient = new RestClient(ApiUrl) {Timeout = -1};
                DefaultRestClient.ClearHandlers();
            }

            if (_mainApiClient != null) return;

            _mainApiClient = new HttpClient {BaseAddress = new Uri(ApiUrl)};
            _mainApiClient.DefaultRequestHeaders.Accept.Clear();
        }

        public static void StopAllOngoingRequests()
        {
            _mainApiClient.CancelPendingRequests();
            DefaultClient.CancelPendingRequests();
        }


        public static void Close()
        {
            StopAllOngoingRequests();

            _mainApiClient.Dispose();
            DefaultClient.Dispose();
            _mainApiClient = null;
            DefaultClient = null;
        }

        private static string FormRequestUri(string requestSuffix, string requestParameters,
            NameValueCollection queryStringParams)
        {
            var queryString = "";
            if (queryStringParams != null)
            {
                queryString = QueryHelpers.MakeAUriQueryString(queryStringParams);
            }

            return $"{_mainApiClient.BaseAddress}{requestSuffix}{requestParameters}{queryString}";
        }

        private static void AddHeaders(HttpRequestMessage requestMessage,
            IEnumerable<KeyValuePair<string, string>> requestHeadersArray)
        {
            foreach (var pair in requestHeadersArray)
            {
                requestMessage.Headers.Add(pair.Key, pair.Value);
            }
        }

        public static async Task<IRestResponse> ExecuteRequestWithDefaultRestClient(RestRequest restRequest, CancellationToken cancellationToken)
        {
            var response = await DefaultRestClient.ExecuteAsync(restRequest, cancellationToken).ConfigureAwait(false);

            if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
            {
                await SimpleAutofac.GetInstance<ISessionController>().ProcessTokenInvalidationCase().ConfigureAwait(false);
                return null;
            }
            LogUtility.PrintLog(Tag, response.ToString());
            return response;
        }

        public static Task<HttpResponseMessage> MakeAsyncRequest(CancellationToken cancellationToken,
            HttpMethod methodType, string requestSuffix, string requestParameters,
            NameValueCollection queryStringParams, List<KeyValuePair<string, string>> requestHeaders,
            object requestBody, bool sendBodyAsQueryStringFormat)
        {
            var requestUri = FormRequestUri(requestSuffix, requestParameters, queryStringParams);
            LogUtility.PrintLog(Tag, $"Request uri: {requestUri}");

            using (var requestMessage = new HttpRequestMessage(methodType, requestUri))
            {
                Assert.IsFalse(requestHeaders == null && requestBody == null);

                void AddBody(object requestBodyObject)
                {
                    requestMessage.Content = CreateStringContent(requestBodyObject);
                }

                void AddBodyAsQueryStringFormat(object requestBodyObject)
                {
                    requestMessage.Content = CreateStringContent(DataModelsUtility.ConvertToQueryStringFormat(requestBodyObject));
                }

                if (requestHeaders != null)
                    AddHeaders(requestMessage, requestHeaders);
                if (requestBody != null)
                {
                    if (sendBodyAsQueryStringFormat)
                    {
                        AddBodyAsQueryStringFormat(requestBody);
                    }
                    else
                    {
                        AddBody(requestBody);
                    }
                }

                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaTypeHeader));
                return _mainApiClient.SendAsync(requestMessage, cancellationToken);
            }
        }

        public static async Task<Task<HttpResponseMessage>> MakeAsyncMultiPartRequest(CancellationToken cancellationToken, HttpMethod methodType,
            string requestSuffix, MultipartFormDataContent formDataContent, List<KeyValuePair<string, string>> requestHeaders)
        {
            Assert.IsFalse(requestHeaders == null && formDataContent == null);

            try
            {
                var asString = await formDataContent.ReadAsStringAsync().ConfigureAwait(false);
                LogUtility.PrintLog(Tag, $"MultipartFormDataContent: {asString}");
                using (var requestMessage = new HttpRequestMessage(methodType, FormRequestUri(requestSuffix, null, null)))
                {
                    AddHeaders(requestMessage, requestHeaders);

                    requestMessage.Content = formDataContent;
                    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MultipartFormData));
                    return _mainApiClient.SendAsync(requestMessage, cancellationToken);
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private static StringContent CreateStringContent(object objectToSerialize)
        {
            var contentAsString = JsonConverterUtility.ConvertModelToJson(objectToSerialize);
            return CreateStringContent(contentAsString);
        }


        private static StringContent CreateStringContent(string content)
        {
            LogUtility.PrintLog(Tag, $"Request message Content : {content}");
            return new StringContent(content, Encoding.UTF8, JsonMediaTypeHeader);
        }
    }
}