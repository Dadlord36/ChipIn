using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface ILoginResponseModel : ISuccess
    {
        [JsonProperty("user")] UserProfileDataModel UserProfileData { get; set; }
        [JsonProperty("auth")] AuthorisationModel AuthorisationData { get; set; }
    }

    public class LoginResponseModel : ILoginResponseModel
    {
        public bool Success { get; set; }
        public UserProfileDataModel UserProfileData { get; set; }
        public AuthorisationModel AuthorisationData { get; set; }
    }
}