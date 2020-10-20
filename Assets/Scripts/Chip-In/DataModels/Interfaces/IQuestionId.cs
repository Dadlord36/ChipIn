using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IQuestionId
    {
        [JsonProperty("question_id")] int QuestionId { get; set; }
    }
}