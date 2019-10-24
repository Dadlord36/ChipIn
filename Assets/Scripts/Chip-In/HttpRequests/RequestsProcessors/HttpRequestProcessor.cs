using System;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace HttpRequests
{
    public static class HttpRequestProcessor
    {
        private static async Task<T> ContentJsonTo<T>(HttpContent content)
        {
            string contentAsString = await content.ReadAsStringAsync();
            return JsonUtility.FromJson<T>(contentAsString);
        }

        public static async Task<T> Get<T>(string requestSuffix)
        {
            using (HttpResponseMessage responseMessage = await ApiHelper.GetAsync(requestSuffix))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    return await ContentJsonTo<T>(responseMessage.Content);
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }

        public static async Task<T> Post<T>(string requestSuffix, object content)
        {
            using (HttpResponseMessage responseMessage = await ApiHelper.PostAsync(requestSuffix, content))
            {
                if (responseMessage.IsSuccessStatusCode)
                {
                    return await ContentJsonTo<T>(responseMessage.Content);
                }
                else
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }
            }
        }
    }
}