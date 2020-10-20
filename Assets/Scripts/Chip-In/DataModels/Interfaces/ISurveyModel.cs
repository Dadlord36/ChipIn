using GlobalVariables;
using Newtonsoft.Json;
using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface ISurveyModel : IIdentifier, ITitled
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Questions)]
        SurveyQuestionDataModel[] Questions { get; set; }
    }
}