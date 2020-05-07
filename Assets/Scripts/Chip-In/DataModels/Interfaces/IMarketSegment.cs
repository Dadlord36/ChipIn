using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IMarketSegment
    {
        [JsonProperty("segment")] string Segment { get; set; }
    }
}