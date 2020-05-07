using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface INamed
    {
        [JsonProperty("name")] string Name { get; set; }
    }
}