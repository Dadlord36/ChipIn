using DataModels.SimpleTypes;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IPosterImageFile
    {
        [JsonProperty("poster")] FilePath PosterFilePath { get; set; }
    }
}