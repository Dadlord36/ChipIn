using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using UnityEngine.Assertions;
using Utilities;

namespace DataModels.HttpRequestsHeadersModels
{
    public interface IRequestHeaders
    {
        List<KeyValuePair<string, string>> GetRequestHeaders();
        string GetRequestHeadersAsString();
    }

    public interface IUserProfileRequestHeadersProvider : IAuthorisationModel, IRequestHeaders
    {
        [JsonProperty("expiry")] int Expiry { get; set; }
        void Set(IUserProfileRequestHeadersProvider source);
    }


    public class UserProfileRequestHeadersProvider : IUserProfileRequestHeadersProvider
    {
        [JsonProperty("access-token")] public string AccessToken { get; set; }
        [JsonProperty("client")] public string Client { get; set; }
        [JsonProperty("token-type")] public string TokenType { get; set; }
        [JsonProperty("uid")] public string Uid { get; set; }
        [JsonProperty("expiry")] public int Expiry { get; set; }

        public void Set(IUserProfileRequestHeadersProvider source)
        {
            Expiry = source.Expiry;
            Set(source as IAuthorisationModel);
        }

        public void Set(IAuthorisationModel source)
        {
            Client = source.Client;
            Uid = source.Uid;
            AccessToken = source.AccessToken;
            TokenType = source.TokenType;
        }


        public List<KeyValuePair<string, string>> GetRequestHeaders()
        {
            return DataModelsUtility.ConvertToKeyValuePairsList(this);
        }
        
        public string GetRequestHeadersAsString()
        {
            var keyValuePairs = GetRequestHeaders();
            var stringBuilder = new StringBuilder();

            foreach (var valuePair in keyValuePairs)
            {
                stringBuilder.Append($"{valuePair.Key} : {valuePair.Value}\n");
            }

            return stringBuilder.ToString();
        }
    }
}