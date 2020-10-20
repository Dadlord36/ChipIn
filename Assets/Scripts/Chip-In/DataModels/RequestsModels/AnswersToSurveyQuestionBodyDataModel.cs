using System.Collections.Generic;
using DataModels.Interfaces;

namespace DataModels.RequestsModels
{
    public class AnswersToSurveyQuestionBodyDataModel : IAnswerToSurveyQuestionBodyModel
    {
        public IList<UserAnswer> UserAnswers { get; set; }
    }
}