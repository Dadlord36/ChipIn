using System.Text;
using Newtonsoft.Json;

namespace Utilities
{
    public static class ErrorsHandlingUtility
    {
        private const string Tag = nameof(ErrorsHandlingUtility);

        private class ApiSimpleErrors
        {
            [JsonProperty("errors")] public string[] ErrorsArray;
        }

        private class ApiStructuredErrors
        {
            public class ErrorModel
            {
                [JsonProperty("key")] public string Key;
                [JsonProperty("messages")] public string Message;
            }

            [JsonProperty("errors")] public ErrorModel[] ErrorData;
        }

        private class ApiDeepStructuredErrors
        {
            public class DeepStructuredError
            {
                [JsonProperty("key")] public string Key;
                [JsonProperty("messages")] public string[] Messages;
            }

            [JsonProperty("errors")] public DeepStructuredError[] ErrorData;
        }

        public static string CollectErrors(string contentAsString)
        {
            if (JsonConverterUtility.TryParseJsonEveryExistingMember<ApiDeepStructuredErrors>(contentAsString,
                out var deepStructuredErrors))
            {
                return BuildErrorStringFromDeepStructuredErrors(deepStructuredErrors.ErrorData);
            }

            if (JsonConverterUtility.TryParseJsonEveryExistingMember<ApiSimpleErrors>(contentAsString,
                out var simpleErrorsData))
            {
                return BuildErrorString(simpleErrorsData.ErrorsArray);
            }

            if (JsonConverterUtility.TryParseJsonEveryExistingMember<ApiStructuredErrors>(contentAsString,
                out var structuredErrorsData))
            {
                return BuildErrorStringFromStructuredErrors(structuredErrorsData.ErrorData);
            }


            string BuildErrorString(string[] errors)
            {
                var stringBuilder = new StringBuilder(errors[0]);
                for (int i = 1; i < errors.Length; i++)
                {
                    stringBuilder.Append($" {errors[i]}");
                }

                return stringBuilder.ToString();
            }

            string BuildErrorStringFromStructuredErrors(ApiStructuredErrors.ErrorModel[] errors)
            {
                var stringBuilder = new StringBuilder(BuildElement(errors[0]));
                for (int i = 1; i < errors.Length; i++)
                {
                    stringBuilder.Append(BuildElement(errors[i]));
                }

                string BuildElement(ApiStructuredErrors.ErrorModel errorElementData)
                {
                    return $"{errorElementData.Key} {errorElementData.Message}";
                }

                return stringBuilder.ToString();
            }

            string BuildErrorStringFromDeepStructuredErrors(ApiDeepStructuredErrors.DeepStructuredError[] errors)
            {
                var stringBuilder = new StringBuilder(BuildElement(errors[0]));
                for (int i = 1; i < errors.Length; i++)
                {
                    stringBuilder.Append(BuildElement(errors[i]));
                }

                string BuildElement(ApiDeepStructuredErrors.DeepStructuredError errorElementData)
                {
                    return $"{errorElementData.Key} {BuildMessageString(errorElementData.Messages)}";

                    string BuildMessageString(string[] messageStrings)
                    {
                        var messageStringBuilder = new StringBuilder(messageStrings[0]);
                        for (int i = 1; i < messageStrings.Length; i++)
                        {
                            messageStringBuilder.Append($" {messageStrings[i]}");
                        }

                        return messageStringBuilder.ToString();
                    }
                }

                return stringBuilder.ToString();
            }

            LogUtility.PrintLogError(Tag, "Given content has no errors");
            return null;
        }
    }
}