using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IBackgroundUrl
    {
        [JsonProperty("background")] string BackgroundUrl { get; set; }
    }
}