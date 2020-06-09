using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Temp
{
    public static class ApiHelper
    {
        private static HttpClient _apiClient;
        private const string ApiUri = "http://chip-in-dev.herokuapp.com/", ApiVersion = "api/v1/";


        public static void InitializeClient()
        {
            if (_apiClient != null) return;

            _apiClient = new HttpClient() /*{BaseAddress = new Uri(ApiUri + ApiVersion)}*/;
            _apiClient.DefaultRequestHeaders.Accept.Clear();
        }

        public static void Dispose()
        {
            _apiClient.Dispose();
            _apiClient = null;
        }

        public static Task<HttpResponseMessage> MakeARequest(HttpMethod methodType, string requestUri, CancellationToken cancellationToken)
        {
            using (var requestMessage = new HttpRequestMessage(methodType, requestUri))
            {
                return  _apiClient.SendAsync(requestMessage, cancellationToken);
            }
        }
    }
}