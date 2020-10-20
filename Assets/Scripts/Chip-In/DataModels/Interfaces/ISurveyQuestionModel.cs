using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ISurveyQuestionModel : IIdentifier
    {
        [JsonProperty("text")] string TextWithQuestion { get; set; }

        [JsonProperty(MainNames.ModelsPropertiesNames.Answers)]
        string[] Answers { get; set; }
    }
}