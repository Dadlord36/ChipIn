using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ICompanyEmail
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.CompanyEmail)]
        string CompanyEmail { get; set; }
    }
}