using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IGameData : IChallengeBaseData
    {
        [JsonProperty("id")] int Id { get; set; }
        [JsonProperty("status")] string Status { get; set; }
        [JsonProperty("users")] string Users { get; set; }
    }
}