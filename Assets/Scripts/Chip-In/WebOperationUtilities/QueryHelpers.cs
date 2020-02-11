﻿using System.Collections.Specialized;
using System.Text;
using Utilities;

namespace WebOperationUtilities
{
    public static class QueryHelpers
    {
        public const string Tag = nameof(QueryHelpers);

        public static string MakeAQueryString(NameValueCollection nameValueDictionary)
        {
            var keys = nameValueDictionary.Keys;
            var stringBuilder = new StringBuilder($"?{FormElement(keys[0])}");

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

            for (int i = 1; i < keys.Count; i++)
            {
                FormElementAndAddItNextToString(keys[i]);
            }

            var resultString = stringBuilder.ToString();

            LogUtility.PrintLog(Tag, $"Request query string: {resultString}");
            return resultString;
        }
    }
}