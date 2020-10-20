using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IAnswerToSurveyQuestionBodyModel
    {
        [JsonProperty("user_answers")]
        IList<UserAnswer> UserAnswers { get; set; }
    }
}