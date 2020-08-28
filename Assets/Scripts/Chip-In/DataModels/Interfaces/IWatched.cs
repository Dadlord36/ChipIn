using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IWatched
    {
        [JsonProperty("watched", NullValueHandling = NullValueHandling.Ignore)] bool Watched { get; set; }
    }
}