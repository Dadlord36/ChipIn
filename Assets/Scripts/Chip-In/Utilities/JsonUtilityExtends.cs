using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utilities
{
    public static class JsonConverterUtility
    {
        public static async Task<T> ConvertAsyncJsonTo<T>(HttpContent content)
        {
            var contentAsString = await content.ReadAsStringAsync();
            return ConvertAsyncJsonTo<T>(contentAsString);
        }

        public static T ConvertAsyncJsonTo<T>(string contentAsString)
        {
            return JsonConvert.DeserializeObject<T>(contentAsString);
        }

        public static string ConvertModelToJson(object model)
        {
            return JsonConvert.SerializeObject(model);
        }

        public static bool TryParseJson<T>(string jsonString, out T result)
        {
            bool? success = true;
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) => { success = false; args.ErrorContext.Handled = true; },
                MissingMemberHandling = MissingMemberHandling.Error
            };
            result = JsonConvert.DeserializeObject<T>(jsonString, settings);
            return (bool) success;
        }
    }
}