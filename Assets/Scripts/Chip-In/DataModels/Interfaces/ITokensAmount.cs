using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ITokensAmount
    {
        [JsonProperty("tokens")] int TokensAmount { get; set; }
    }
}