using System;
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

    [Serializable]
    public struct DeviceData
    {
        public DeviceData(string platform, string deviceId, string deviceToken)
        {
            this.platform = platform;
            this.deviceId = deviceId;
            this.deviceToken = deviceToken;
        }

        public string platform;
        public string deviceId;
        public string deviceToken;
    }

    [Serializable]
    public class UserRegistrationModel : UserSimpleRegistrationModel, IUserRegistrationModel
    {
        [SerializeField] private string gender;
        [SerializeField] private string role;
        [SerializeField] private DeviceData device;
        
        public string Gender
        {
            get => gender;
            set => gender = value;
        }

        public string Role
        {
            get => role;
            set => role = value;
        }

        public DeviceData Device
        {
            get => device;
            set => device = value;
        }
    }
}