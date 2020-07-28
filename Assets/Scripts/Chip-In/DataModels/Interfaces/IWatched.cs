using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IWatched
    {
        [JsonProperty("watched")] bool Watched { get; set; }
    }
}