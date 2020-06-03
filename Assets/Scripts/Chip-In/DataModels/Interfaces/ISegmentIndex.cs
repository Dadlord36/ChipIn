using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ISegmentIndex
    {
        [JsonProperty("segment")] int SegmentIndex { get; set; }
    }
}