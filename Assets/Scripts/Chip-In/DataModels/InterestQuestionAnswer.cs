using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataModels
{
    public class InterestQuestionAnswer
    {
        [JsonProperty("question")] public string Question { get; set; }
        [JsonProperty("answers")] public IList<AnswerData> Answers { get; set; }
    }
}