﻿using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            string requestParameters, NameValueCollection queryStringParams, List<KeyValuePair<string, string>>
                requestHeaders, object requestBody, bool sendBodyAsQueryStringFormat)
        {
            var queryString = "";
            if (queryStringParams != null)
            {
                queryString = QueryHelpers.MakeAUriQueryString(queryStringParams);
            }

            string requestUri = $"{_apiClient.BaseAddress}{requestSuffix}{requestParameters}{queryString}";

            LogUtility.PrintLog(Tag, $"Request uri: {requestUri}");

            using (var requestMessage = new HttpRequestMessage(methodType, requestUri))
            {
                Assert.IsFalse(requestHeaders == null && requestBody == null);

                void AddHeaders(List<KeyValuePair<string, string>> requestHeadersArray)
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

                void AddBodyAsQueryStringFormat(object requestBodyObject)
                {
                    requestMessage.Content =
                        CreateStringContent(DataModelsUtility.ConvertToQueryStringFormat(requestBodyObject));
                }

                if (requestHeaders != null)
                    AddHeaders(requestHeaders);
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

                return await _apiClient.SendAsync(requestMessage);
            }
        }

        private static StringContent CreateStringContent(object objectToSerialize)
        {
            var contentAsString = JsonConvert.SerializeObject(objectToSerialize);

            return CreateStringContent(contentAsString);
        }

        private static StringContent CreateStringContent(string content)
        {
            LogUtility.PrintLog(Tag, $"Request message Content : {content}");
            return new StringContent(content, Encoding.UTF8, JsonMediaTypeHeader);
        }
    }
}