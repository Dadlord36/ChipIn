using System.Collections.Specialized;
using System.Text;
using Utilities;

namespace WebOperationUtilities
{
    public static class QueryHelpers
    {
        private const string Tag = nameof(QueryHelpers);

        public static string MakeAUriQueryString(NameValueCollection nameValueDictionary)
        {
            var resultString = $"?{ConvertToAQueryStringFormat(nameValueDictionary)}";

            LogUtility.PrintLog(Tag, $"Request query string: {resultString}");
            return resultString;
        }

        public static string ConvertToAQueryStringFormat(NameValueCollection nameValueDictionary)
        {
            var keys = nameValueDictionary.Keys;
            var stringBuilder = new StringBuilder();
            
            string FormElement(in string key)
            {
                return $"{key}={nameValueDictionary[key]}";
            }
            
            void AddNextElement(string element)
            {
                stringBuilder.Append($"&{element}");
            }
            
            void FormElementAndAddItNextToString(in string key)
            {
                AddNextElement(FormElement(key));
            }
            
            if (keys.Count < 1) return stringBuilder.ToString();

            stringBuilder.Append(FormElement(keys[0]));

            for (var i = 1; i < keys.Count; i++)
            {
                FormElementAndAddItNextToString(keys[i]);
            }
            
            var resultString = stringBuilder.ToString();
            
            LogUtility.PrintLog(Tag, $"Formed query string: {resultString}");
            return resultString;
        }
    }
}