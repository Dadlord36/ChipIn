using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IInterestQuestionAnswerRequestResponseModel
    {
        [JsonProperty("questions")] InterestQuestionAnswerDataModel[] Questions { get; set; }
    }
    
}