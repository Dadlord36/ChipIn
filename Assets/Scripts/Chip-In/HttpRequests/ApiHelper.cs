using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine.Assertions;
using Utilities;

namespace HttpRequests
{
    public static class ApiHelper
    {
        private static HttpClient _apiClient;
        private const string JsonMediaTypeHeader = "application/json";

        private const string ApiUri = "http://chip-in-dev.herokuapp.com/", ApiVersion = "api/v1/";
//        private const string ApiUri = "https://reqres.in/", ApiVersion = "";

        public static void InitializeClient()
        {
            _apiClient = new HttpClient {BaseAddress = new Uri(ApiUri + ApiVersion)};

            _apiClient.DefaultRequestHeaders.Accept.Clear();
            _apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(JsonMediaTypeHeader));
        }

        public static void Dispose()
        {
            _apiClient.Dispose();
        }

        public static async Task<HttpResponseMessage> MakeAsyncRequest(HttpMethod methodType, string requestSuffix,
           List<KeyValuePair<string, string>> requestHeaders, object requestBody)
        {
            using (var requestMessage = new HttpRequestMessage(methodType, _apiClient.BaseAddress + requestSuffix))
            {
                Assert.IsFalse(requestHeaders == null && requestBody == null);

                void AddHeaders(List<KeyValuePair<string, string>>  requestHeadersArray)
                {
                    foreach (var pair in requestHeadersArray)
                    {
                        requestMessage.Headers.Add(pair.Key, pair.Value);
                    }
                }

                void AddBody(object requestBodyObject)
                {
                    requestMessage.Content = CreateStringContent(requestBodyObject);
                }

                if (requestHeaders != null)
                    AddHeaders(requestHeaders);
                if (requestBody != null)
                    AddBody(requestBody);

                return await _apiClient.SendAsync(requestMessage);
            }
        }

        private static StringContent CreateStringContent(object objectToSerialize)
        {
            return new StringContent(JsonConvert.SerializeObject(objectToSerialize), Encoding.UTF8,
                JsonMediaTypeHeader);
        }

        [Obsolete]
        public static async Task<HttpResponseMessage> GetAsync(string requestSuffix, object requestHeaders)
        {
            using (var requestMessage = new HttpRequestMessage(HttpMethod.Get, _apiClient.BaseAddress + requestSuffix))
            {
                requestMessage.Content = CreateStringContent(requestHeaders);
                return await _apiClient.SendAsync(requestMessage);
            }
        }
        
        [Obsolete]
        public static async Task<HttpResponseMessage> PostAsync(string requestSuffix, object requestModel)
        {
            return await _apiClient.PostAsync(requestSuffix, CreateStringContent(requestModel));
        }
    }
}