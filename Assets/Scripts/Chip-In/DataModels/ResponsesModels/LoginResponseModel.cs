using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface ILoginResponseModel : ISuccess
    {
        [JsonProperty("user")] UserProfileDataWebModel UserProfileData { get; set; }
        [JsonProperty("auth")] AuthorisationModel AuthorisationData { get; set; }
    }

    public class LoginResponseModel : ILoginResponseModel
    {
        public bool Success { get; set; }
        public UserProfileDataWebModel UserProfileData { get; set; }
        public AuthorisationModel AuthorisationData { get; set; }
    }
}