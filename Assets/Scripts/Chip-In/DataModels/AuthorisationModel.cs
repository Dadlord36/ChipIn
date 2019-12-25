using Newtonsoft.Json;

namespace DataModels
{
    public interface IAuthorisationModel
    {
        [JsonProperty("access-token")] string AccessToken { get; set; }
        [JsonProperty("client")] string Client { get; set; }
        [JsonProperty("token-type")] string TokenType { get; set; }
        [JsonProperty("uid")] string Uid { get; set; }
        void Set(IAuthorisationModel source);
    }

    public class AuthorisationModel : IAuthorisationModel
    {
        public string AccessToken { get; set; }
        public string Client { get; set; }
        public string TokenType { get; set; }
        public string Uid { get; set; }

        public void Set(IAuthorisationModel source)
        {
            AccessToken = source.AccessToken;
            Client = source.Client;
            TokenType = source.TokenType;
            Uid = source.Uid;
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}