using System;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IChallengeBaseModel
    {
        [JsonProperty("challenge_type")]  string ChallengeType { get; set; }
        [JsonProperty("started_at")]  DateTime StartedAt { get; set; }
    }
}