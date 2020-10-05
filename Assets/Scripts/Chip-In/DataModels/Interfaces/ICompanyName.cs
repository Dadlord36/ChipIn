using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ICompanyName
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.CompanyName)]
        string CompanyName { get; set; }
    }
}