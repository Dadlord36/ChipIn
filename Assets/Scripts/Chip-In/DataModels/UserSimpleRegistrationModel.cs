using Newtonsoft.Json;

namespace DataModels
{

    public class UserSimpleRegistrationModel
    {
        private string email;
        private string password;

        [JsonProperty("email")]
        public string Email
        {
            get => email;
            set => email = value;
        }

        [JsonProperty("password")]
        public string Password
        {
            get => password;
            set => password = value;
        }
    }
}