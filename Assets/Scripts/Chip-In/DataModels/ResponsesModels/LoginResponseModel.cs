using Newtonsoft.Json;

namespace DataModels.ResponsesModels
{
    public interface ILoginResponseModel
    {
        [JsonProperty("success")] bool RequestIsSuccessful { get; set; }
        [JsonProperty("user")] UserProfileDataWebModel UserProfileData { get; set; }

        [JsonProperty("auth")] AuthorisationModel AuthorisationData { get; set; }
    }

    public class LoginResponseModel : ILoginResponseModel
    {
        public bool RequestIsSuccessful { get; set; }
        public UserProfileDataWebModel UserProfileData { get; set; }
        public AuthorisationModel AuthorisationData { get; set; }
    }
}