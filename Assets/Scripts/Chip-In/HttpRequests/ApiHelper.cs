using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

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

        public static async Task<HttpResponseMessage> GetAsync(string requestSuffix)
        {
            return await _apiClient.GetAsync(requestSuffix);
        }

        public static async Task<HttpResponseMessage> PostAsync(string requestSuffix, object content)
        {
            var json = JsonUtility.ToJson(content);
            return await _apiClient.PostAsync(requestSuffix, new StringContent(json, Encoding.UTF8, JsonMediaTypeHeader));
        }
        
    }
}