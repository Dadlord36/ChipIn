using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.Assertions;
using Utilities;
using WebOperationUtilities;

namespace HttpRequests
{
    public static class ApiHelper
    {
        private const string Tag = nameof(ApiHelper);

        private static HttpClient _apiClient;
        private const string JsonMediaTypeHeader = "application/json";
        private const string MultipartFormData = "multipart/form-data";

        private const string ApiUri = "http://chip-in-dev.herokuapp.com/", ApiVersion = "api/v1/";
//        private const string ApiUri = "https://reqres.in/", ApiVersion = "";

        public static void InitializeClient()
        {
            if (_apiClient != null) return;

            _apiClient = new HttpClient {BaseAddress = new Uri(ApiUri + ApiVersion)};
            _apiClient.DefaultRequestHeaders.Accept.Clear();
        }

        public static void Dispose()
        {
            _apiClient.Dispose();
        }

        private static string FormRequestUri(string requestSuffix, string requestParameters,
            NameValueCollection queryStringParams)
        {
            var queryString = "";
            if (queryStringParams != null)
            {
                queryString = QueryHelpers.MakeAUriQueryString(queryStringParams);
            }

            return $"{_apiClient.BaseAddress}{requestSuffix}{requestParameters}{queryString}";
        }

        private static void AddHeaders(HttpRequestMessage requestMessage,
            IEnumerable<KeyValuePair<string, string>> requestHeadersArray)
        {
            foreach (var pair in requestHeadersArray)
            {
                requestMessage.Headers.Add(pair.Key, pair.Value);
            }
        }

        public static async Task<HttpResponseMessage> MakeAsyncRequest(HttpMethod methodType, string requestSuffix,
            string requestParameters, NameValueCollection queryStringParams, List<KeyValuePair<string, string>>
                requestHeaders, object requestBody, bool sendBodyAsQueryStringFormat)
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
                    requestMessage.Content =
                        CreateStringContent(DataModelsUtility.ConvertToQueryStringFormat(requestBodyObject));
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
                return await _apiClient.SendAsync(requestMessage);
            }
        }

        public static async Task<HttpResponseMessage> MakeAsyncMultiPartRequest(HttpMethod methodType,
            string requestSuffix, MultipartFormDataContent formDataContent, List<KeyValuePair<string, string>>
                requestHeaders)
        {
            Assert.IsFalse(requestHeaders == null && formDataContent == null);

            var asString = await formDataContent.ReadAsStringAsync();
            LogUtility.PrintLog(Tag, $"MultipartFormDataContent: {asString}");
            using (var requestMessage = new HttpRequestMessage(methodType, FormRequestUri(requestSuffix, null, null)))
            {
                AddHeaders(requestMessage, requestHeaders);
                requestMessage.Content = formDataContent;
                requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue(MultipartFormData));
                return await _apiClient.SendAsync(requestMessage);
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