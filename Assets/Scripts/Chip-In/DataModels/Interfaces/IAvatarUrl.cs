using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IAvatarUrl
    {
        [JsonProperty("avatar")] string AvatarUrl { get; set; }
    }
}