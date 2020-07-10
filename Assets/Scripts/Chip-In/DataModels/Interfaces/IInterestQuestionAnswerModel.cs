using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IInterestQuestionAnswerModel
    {
        [JsonProperty("question")] string Question { get; set; }
        [JsonProperty("answers")] InterestQuestionAnswer[] Answers { get; set; }
    }
}