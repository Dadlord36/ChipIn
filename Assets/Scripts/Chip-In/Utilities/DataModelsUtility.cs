using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using DataModels.SimpleTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using UnityEngine.Assertions;
using WebOperationUtilities;

namespace Utilities
{
    public static class DataModelsUtility
    {
        public static List<KeyValuePair<string, string>> ConvertToKeyValuePairsList(object dataModel)
        {
            return ToKeyValue(dataModel).ToList();
        }

        public static List<string> ToHeaderForm(object dataModel, string parent)
        {
            var keyValuesList = ConvertToKeyValuePairsList(dataModel);
            var listOfStrings = new List<string>(keyValuesList.Count);
            listOfStrings.AddRange(collection: keyValuesList.Select(pair => ToHeaderForm(pair, parent)));

            return listOfStrings;
        }

        public static string ToHeaderForm(KeyValuePair<string, string> field, string parent)
        {
            return $"{parent}[{field.Key}] : {field.Value}";
        }

        private struct FilePathAndStream
        {
            public Stream FileStream;
            public readonly string FileName;

            public FilePathAndStream(FilePath filePath)
            {
                FileName = Path.GetFileName(filePath.Path);
                Assert.IsFalse(string.IsNullOrEmpty(filePath.Path));
                FileStream = new FileStream(filePath.Path, FileMode.Open);
            }
        }

        /*public static MultipartFormDataContent ConvertObjectToMultipartFormDataContent(object dataModel, string boundary)
        {
            var properties = dataModel.GetType().GetProperties();
            var dataProperties = new List<KeyValuePair<string, string>>(properties.Length);

            // var filesStreams = new List<KeyValuePair<string, FilePathAndStream>>();
            var multipartFormDataContent = new MultipartContent(boundary);

            for (int i = 0; i < properties.Length; i++)
            {
                var attributes = properties[i].GetCustomAttributes(true);
                
                var propertyValue = properties[i].GetValue(dataModel);
                foreach (var attribute in attributes)
                {
                    var jsonPropertyAttribute = attribute as JsonPropertyAttribute;
                    Assert.IsNotNull(jsonPropertyAttribute);

                    var propertyName = jsonPropertyAttribute.PropertyName;
                    
                    
                    
                    // if (properties[i].PropertyType != typeof(FilePath))
                        dataProperties.Add(new KeyValuePair<string, string>(propertyName,
                            JsonConverterUtility.ConvertModelToJson(propertyValue)));
                    /*else
                    {
                        var filePath = (FilePath)propertyValue ;
                        filesStreams.Add(new KeyValuePair<string, FilePathAndStream>(propertyName,
                            new FilePathAndStream(filePath)));
                    }#1#
                }
            }

            /*foreach (var filesStream in filesStreams)
            {
                var streamContent = new StreamContent(filesStream.Value.FileStream);
                streamContent.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data")
                {
                    Name = filesStream.Key, FileName = filesStream.Value.FileName
                };

                multipartFormDataContent.Add(streamContent);
            }#1#

            foreach (var dataProperty in dataProperties)
            {
                multipartFormDataContent.Add(new StringContent(dataProperty.Value, Encoding.UTF8),dataProperty.Key);
            }
            
            return multipartFormDataContent;
        }*/


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

        public static FormUrlEncodedContent ToFormData(object obj)
        {
            return new FormUrlEncodedContent(ConvertToKeyValuePairsList(obj));
        }

        public static MultipartFormDataContent ConvertFlatModelToMultipartFormDataContent(string rootPropertyName, object model)
        {
            var properties = model.GetType().GetProperties();
            var multipartForm = new MultipartFormDataContent();

            foreach (var property in properties)
            {
                if (property.PropertyType == typeof(FilePath))
                {
                    var value = property.GetValue(model);
                    if (value == null) continue;

                    multipartForm.Add(new StreamContent(File.OpenRead(((FilePath) value).Path)),
                        FormPropertyName(GetJsonPropertyName(property)));
                }
                else
                {
                    var keyValuePair = ConvertPropertyToKeyValuePair(property, model);
                    multipartForm.Add(new StringContent(keyValuePair.Value), FormPropertyName(keyValuePair.Key));
                }
            }

            string FormPropertyName(in string propertyName)
            {
                return $"{rootPropertyName}[{propertyName}]";
            }

            string GetJsonPropertyName(MemberInfo propertyInfo)
            {
                var attribute = (JsonPropertyAttribute) Attribute.GetCustomAttribute(propertyInfo, typeof(JsonProperty));
                return attribute.PropertyName;
            }

            KeyValuePair<string, string> ConvertPropertyToKeyValuePair(PropertyInfo property, object owner)
            {
                return new KeyValuePair<string, string>(GetJsonPropertyName(property), property.GetValue(owner).ToString());
            }

            return multipartForm;
        }

        public static IDictionary<string, string> ToKeyValue(object metaToken)
        {
            if (metaToken == null)
            {
                return null;
            }

            // Added by me: avoid cyclic references
            var serializer = new JsonSerializer {ReferenceLoopHandling = ReferenceLoopHandling.Ignore};
            var token = metaToken as JToken;
            if (token == null)
            {
                // Modified by me: use serializer defined above
                return ToKeyValue(JObject.FromObject(metaToken, serializer));
            }

            if (token.HasValues)
            {
                var contentData = new Dictionary<string, string>();
                foreach (var child in token.Children().ToList())
                {
                    var childContent = ToKeyValue(child);
                    if (childContent != null)
                    {
                        contentData = contentData.Concat(childContent)
                            .ToDictionary(k => k.Key, v => v.Value);
                    }
                }

                return contentData;
            }

            var jValue = token as JValue;
            if (jValue?.Value == null)
            {
                return null;
            }

            var value = jValue?.Type == JTokenType.Date
                ? jValue?.ToString("o", CultureInfo.InvariantCulture)
                : jValue?.ToString(CultureInfo.InvariantCulture);

            return new Dictionary<string, string> {{token.Path, value}};
        }
    }
}