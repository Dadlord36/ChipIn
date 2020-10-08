using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ISegmentName
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Segment)] string SegmentName { get; set; }
    }
}