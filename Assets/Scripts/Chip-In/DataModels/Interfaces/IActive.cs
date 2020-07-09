using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IActive
    {
        [JsonProperty("active")] bool Active { get; set; }
    }
}