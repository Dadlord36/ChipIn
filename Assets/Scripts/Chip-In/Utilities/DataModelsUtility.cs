using System.Collections.Generic;
using System.Collections.Specialized;
using Newtonsoft.Json;
using UnityEngine.Assertions;
using WebOperationUtilities;

namespace Utilities
{
    public static class DataModelsUtility
    {
        public static List<KeyValuePair<string, string>> ConvertToKeyValuePairsList(object dataModel)
        {
            var properties = dataModel.GetType().GetProperties();
            var headers = new List<KeyValuePair<string, string>>(properties.Length);

            for (int i = 0; i < properties.Length; i++)
            {
                foreach (var attribute in properties[i].GetCustomAttributes(true))
                {
                    var jsonPropertyAttribute = attribute as JsonPropertyAttribute;
                    Assert.IsNotNull(jsonPropertyAttribute);

                    headers.Add(new KeyValuePair<string, string>(jsonPropertyAttribute.PropertyName,
                        properties[i].GetValue(dataModel).ToString()));
                }
            }

            return headers;
        }

        public static NameValueCollection ConvertToNameValueCollection(object dataModel)
        {
            var properties = dataModel.GetType().GetProperties();
            var collection = new NameValueCollection(properties.Length);

            for (int i = 0; i < properties.Length; i++)
            {
                foreach (var attribute in properties[i].GetCustomAttributes(true))
                {
                    var jsonPropertyAttribute = attribute as JsonPropertyAttribute;
                    Assert.IsNotNull(jsonPropertyAttribute);

                    collection.Add(jsonPropertyAttribute.PropertyName, properties[i].GetValue(dataModel).ToString());
                }
            }

            return collection;
        }

        public static string ConvertToQueryStringFormat(object dataModel)
        {
            return QueryHelpers.ConvertToAQueryStringFormat(ConvertToNameValueCollection(dataModel));
        }
    }
}