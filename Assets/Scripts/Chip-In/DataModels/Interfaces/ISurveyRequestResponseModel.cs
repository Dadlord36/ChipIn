using GlobalVariables;
using HttpRequests.RequestsProcessors.GetRequests;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface ISurveyRequestResponseModel : ISuccess
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Survey)]
        SurveyDataModel Survey { get; set; }
    }
}