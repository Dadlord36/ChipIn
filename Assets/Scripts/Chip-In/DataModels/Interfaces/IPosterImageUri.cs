using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IPosterImageUri
    {
        [JsonProperty("poster")] string PosterUri { get; set; }
    }
}