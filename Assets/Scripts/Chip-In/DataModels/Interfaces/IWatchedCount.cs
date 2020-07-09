using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IWatchedCount
    {
        [JsonProperty("watched_count")] uint WatchedCount { get; set; }
    }
}