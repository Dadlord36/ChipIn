using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ITokensAmount
    {
        [JsonProperty("tokens")] uint TokensAmount { get; set; }
    }
}