using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ICategory
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Category)]
        string Category { get; set; }
    }
}