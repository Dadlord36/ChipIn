using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IFoundedCount
    {
        [JsonProperty("funded_count")] string FoundedCount { get; set; }
    }
}