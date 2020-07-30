using Newtonsoft.Json;

namespace DataModels
{
    public class AnswerData
    {
        [JsonProperty("answer")] public string Answer { get; set; }
        [JsonProperty("percent")] public uint Percent { get; set; }
    }
}