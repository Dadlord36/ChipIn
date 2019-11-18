using System.Net.Http;
using System.Threading.Tasks;

namespace HttpRequests.RequestsProcessors
{
    public static class HttpRequestProcessor
    {
        public static async Task<HttpResponseMessage> Get(string requestSuffix)
        {
            return await ApiHelper.GetAsync(requestSuffix);
        }

        public static async Task<HttpResponseMessage> Post(string requestSuffix, object content)
        {
            return await ApiHelper.PostAsync(requestSuffix, content);
        }
    }
}