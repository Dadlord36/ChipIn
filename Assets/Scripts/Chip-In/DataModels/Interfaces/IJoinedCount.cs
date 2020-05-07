using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IJoinedCount
    {
        [JsonProperty("joined_count")] uint JoinedCount { get; set; }
    }
}