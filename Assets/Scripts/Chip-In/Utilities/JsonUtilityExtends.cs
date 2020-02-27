using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utilities
{
    public static class JsonConverterUtility
    {
        private const string Tag = nameof(JsonConverterUtility);
        public static async Task<T> ConvertHttpContentAsync<T>(HttpContent content)
        {
            var contentAsString = await content.ReadAsStringAsync();
            return ConvertJsonString<T>(contentAsString);
        }

        public static T ConvertJsonString<T>(string contentAsString)
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
                Error = (sender, args) =>
                {
                    success = false;
                    args.ErrorContext.Handled = true;
                },
                MissingMemberHandling = MissingMemberHandling.Error
            };
            result = DeserializeJsonStringWithParameters<T>(jsonString, settings);
            return (bool) success;
        }

        public static bool TryParseJsonEveryExistingMember<T>(string jsonString, out T result)
        {
            bool? success = true;
            var settings = new JsonSerializerSettings
            {
                Error = (sender, args) =>
                {
                    success = false;
                    args.ErrorContext.Handled = true;
                    LogUtility.PrintLog(Tag,args.ErrorContext.Error.Message);
                },
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            result = DeserializeJsonStringWithParameters<T>(jsonString, settings);
            return (bool) success;
        }

        private static T DeserializeJsonStringWithParameters<T>(string jsonString, JsonSerializerSettings settings)
        {
            return JsonConvert.DeserializeObject<T>(jsonString, settings);
        }
    }
}