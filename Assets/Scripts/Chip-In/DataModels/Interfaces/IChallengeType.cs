using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IChallengeType
    {
        [JsonProperty("challenge_type")] string ChallengeType { get; set; }
    }
}