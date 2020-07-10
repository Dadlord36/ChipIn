using DataModels.Interfaces;

namespace DataModels
{
    public class InterestQuestionAnswerDataModel : IInterestQuestionAnswerModel
    {
        public string Question { get; set; }
        public InterestQuestionAnswer[] Answers { get; set; }
    }
}