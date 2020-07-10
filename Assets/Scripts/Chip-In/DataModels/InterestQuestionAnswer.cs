using Newtonsoft.Json;

namespace DataModels
{
    public class InterestQuestionAnswer
    {
        [JsonProperty("answer")]
        public string Answer { get; set; }

        [JsonProperty("percent")]
        public uint Percent { get; set; }
    }

}