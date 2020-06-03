using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IIsPublic
    {
        [JsonProperty("is_public")] bool IsPublic { get; set; }
    }
}