using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IUsersCount
    {
        [JsonProperty("users_count")] uint UsersCount { get; set; }
    }
}