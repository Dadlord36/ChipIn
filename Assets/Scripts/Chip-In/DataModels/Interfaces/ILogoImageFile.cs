using DataModels.SimpleTypes;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ILogoImageFile
    {
        [JsonProperty("logo")] FilePath LogoFilePath { get; set; }
    }
}