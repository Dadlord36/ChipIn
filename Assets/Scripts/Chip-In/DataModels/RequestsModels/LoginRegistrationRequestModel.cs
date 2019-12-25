using Newtonsoft.Json;

namespace DataModels.RequestsModels
{
    public interface IUserRegistrationModel : IBasicLoginModel
    {
        [JsonProperty("gender")] string Gender { get; set; }
        [JsonProperty("role")] string Role { get; set; }
        [JsonProperty("device")] DeviceData Device { get; set; }
    }

    // public interface ILoginRegistrationModel : {}


    public struct DeviceData
    {
        public DeviceData(string platform, string deviceId, string deviceToken)
        {
            Platform = platform;
            DeviceId = deviceId;
            DeviceToken = deviceToken;
        }

        [JsonProperty] public string Platform;
        [JsonProperty("device_id")] public string DeviceId;
        [JsonProperty("device_token")] public string DeviceToken;
    }


    public class RegistrationRequestModel : SimpleRegistrationRequestModel, IUserRegistrationModel
    {
        public string Gender { get; set; }
        public string Role { get; set; }
        public DeviceData Device { get; set; }
    }
}