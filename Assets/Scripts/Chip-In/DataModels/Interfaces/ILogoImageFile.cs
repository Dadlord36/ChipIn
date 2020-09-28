using DataModels.SimpleTypes;
using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ILogoImageFile
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Logo)] FilePath LogoFilePath { get; set; }
    }
}