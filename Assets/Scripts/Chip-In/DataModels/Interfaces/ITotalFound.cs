using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ITotalFound
    {
        [JsonProperty("total_fund")] string TotalFound { get; set; }
    }
}