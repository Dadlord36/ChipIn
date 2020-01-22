using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IChallengeBaseData
    {
        [JsonProperty("challenge_type")]  string ChallengeType { get; set; }
        [JsonProperty("started_at")]  string StartedAt { get; set; }
    }
}