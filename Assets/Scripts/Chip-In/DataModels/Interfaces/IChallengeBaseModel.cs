using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataModels.Interfaces
{
    public interface IChallengeBaseModel : IStartedAtTime
    {
        [JsonProperty("challenge_type")] string ChallengeType { get; set; }
    }


}