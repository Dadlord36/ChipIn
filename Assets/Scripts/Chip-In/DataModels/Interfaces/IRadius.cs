using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IRadius
    {
        [JsonProperty("radius")] string Radius { get; set; }
    }
}