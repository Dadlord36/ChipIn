using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IUserInterestPageActions
    {
        [JsonProperty("watch")] bool Watch { get; set; }

        [JsonProperty("join")] bool Join { get; set; }

        [JsonProperty("support")] bool Support { get; set; }

        [JsonProperty("fund")] bool Fund { get; set; }

        [JsonProperty("suggest_survey")] bool SuggestSurvey { get; set; }
    }
}