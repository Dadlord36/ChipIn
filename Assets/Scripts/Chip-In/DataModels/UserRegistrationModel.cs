using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using UnityEngine;

namespace DataModels
{
    public interface IUserRegistrationModel : IUserSimpleRegistrationModel
    {
        string Gender { get; set; }
        string Role { get; set; }
    }

    public interface IUserSimpleRegistrationModel
    {
        string Email { get; set; }
        string Password { get; set; }
    }


    public struct DeviceData
    {
        public DeviceData(string platform, string deviceId, string deviceToken)
        {
            this.platform = platform;
            this.deviceId = deviceId;
            this.deviceToken = deviceToken;
        }

        [JsonProperty] public string platform;
        [JsonProperty("device_id")] public string deviceId;
        [JsonProperty("device_token")] public string deviceToken;
    }


    public class UserRegistrationModel : UserSimpleRegistrationModel, IUserRegistrationModel
    {
        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("role")]
        public string Role { get; set; }

        [JsonProperty("device")]
        public DeviceData Device { get; set; }
    }
}