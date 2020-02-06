using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utilities
{
    public static class JsonConverterUtility
    {
        public static async Task<T> ContentAsyncJsonTo<T>(HttpContent content)
        {
            var contentAsString = await content.ReadAsStringAsync();
            return ContentAsyncJsonTo<T>(contentAsString);
        }
        
        public static T ContentAsyncJsonTo<T>(string contentAsString)
        {
            return JsonConvert.DeserializeObject<T>(contentAsString);
        }
    }
}