using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ITotalFound
    {
        [JsonProperty("total_fund")] int TotalFound { get; set; }
    }
}