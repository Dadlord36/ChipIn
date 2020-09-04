using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ICommunitySpirit
    {
        [JsonProperty("market_spirit")] string Spirit { get; set; }
    }

    public interface IMarketCap
    {
        [JsonProperty("min_cap")] uint MinCap { get; set; }
        [JsonProperty("max_cap")] uint MaxCap { get; set; }
    }
    
    public interface IMarketModel : IMarketCap, ICommunitySpirit
    {
        [JsonProperty("size")] uint Size { get; set; }
        [JsonProperty("age")] string Age { get; set; }
        
    }
}