using System.Collections.Generic;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IInterestAnswersRequestModel : ISuccess
    {
        [JsonProperty("answers")] IList<InterestQuestionAnswer> Answers { get; set; }
    }
}