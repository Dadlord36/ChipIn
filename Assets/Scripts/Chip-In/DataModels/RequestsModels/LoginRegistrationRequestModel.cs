using DataModels.Interfaces;
using Newtonsoft.Json;

namespace DataModels.RequestsModels
{
    public interface IRole
    {
        [JsonProperty("role")] string Role { get; set; }
    }

    public interface IGender
    {
        [JsonProperty("gender")] string Gender { get; set; }
    }

    public interface IDevice
    {
        [JsonProperty("device")] DeviceData Device { get; set; }
    }

    public interface IUserRegistrationModel : IBasicLoginModel, IGender, IRole, IDevice
    {
    }

    public interface IBaseDeviceData
    {
        [JsonProperty("device_id")] string DeviceId { get; set; }
        [JsonProperty("platform")] string Platform { get; set; }
    }


    public interface IDeviceData : IBaseDeviceData
    {
        [JsonProperty("device_token")] string DeviceToken { get; set; }
    }


    public class DeviceData : IDeviceData
    {
        public DeviceData(string deviceId, string platform, string deviceToken)
        {
            DeviceId = deviceId;
            Platform = platform;
            DeviceToken = deviceToken;
        }

        public string DeviceId { get; set; }
        public string Platform { get; set; }
        public string DeviceToken { get; set; }
    }


    public class RegistrationRequestModel : SimpleRegistrationRequestModel, IUserRegistrationModel
    {
        public string Gender { get; set; }
        public string Role { get; set; }
        public DeviceData Device { get; set; }
    }
}