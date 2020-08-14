using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IIdentifier
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Id)] int? Id { get; set; }
    }
}