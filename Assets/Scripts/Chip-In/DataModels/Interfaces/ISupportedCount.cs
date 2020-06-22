using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ISupportedCount
    {
        [JsonProperty("supported_count")] uint SupportedCount { get; set; }
    }
}