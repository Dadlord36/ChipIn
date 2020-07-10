using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class InterestQuestionAnswerRequestResponse : IInterestQuestionAnswerRequestResponseModel
    {
        public InterestQuestionAnswerDataModel[] Questions { get; set; }
    }
}