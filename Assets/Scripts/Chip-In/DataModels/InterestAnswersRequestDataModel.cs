using System.Collections.Generic;
using DataModels.Interfaces;

namespace DataModels
{
    public class InterestAnswersRequestDataModel : IInterestAnswersRequestModel
    {
        public bool Success { get; set; }

        public IList<InterestQuestionAnswer> Answers { get; set; }
    }
}