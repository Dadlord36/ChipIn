using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ISegmentIndex
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Segment)] int SegmentIndex { get; set; }
    }
}