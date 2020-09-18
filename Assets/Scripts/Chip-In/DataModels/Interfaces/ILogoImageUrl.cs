using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ILogoImageUrl
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Logo)] string LogoUrl { get; set; }
    }
}