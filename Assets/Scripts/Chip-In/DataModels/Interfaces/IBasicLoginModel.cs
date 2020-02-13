using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IBasicLoginModel
    {
        [JsonProperty("email")] string Email { get; set; }
        [JsonProperty("password")] string Password { get; set; }
    }
}