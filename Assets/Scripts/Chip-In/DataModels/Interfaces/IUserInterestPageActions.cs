using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IUserInterestPageActions
    {
        [JsonProperty("watch", NullValueHandling = NullValueHandling.Ignore)] bool Watch { get; set; }

        [JsonProperty("join", NullValueHandling = NullValueHandling.Ignore)] bool Join { get; set; }

        [JsonProperty("support", NullValueHandling = NullValueHandling.Ignore)] bool Support { get; set; }

        [JsonProperty("fund", NullValueHandling = NullValueHandling.Ignore)] bool Fund { get; set; }

        [JsonProperty("suggest_survey", NullValueHandling = NullValueHandling.Ignore)] bool SuggestSurvey { get; set; }
    }
}