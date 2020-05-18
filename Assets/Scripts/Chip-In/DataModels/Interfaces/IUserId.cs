using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IUserId
    {
        [JsonProperty("user_id")] int UserId { get; set; }
    }
}