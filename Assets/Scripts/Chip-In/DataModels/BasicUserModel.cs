using Newtonsoft.Json;

namespace DataModels
{
    public interface IBasicLoginModel
    {
        [JsonProperty("email")] string Email { get; set; }
        [JsonProperty("password")] string Password { get; set; }
    }

    public class BasicLoginModel : IBasicLoginModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}