using DataModels;
using UnityEngine;
using Utilities;

namespace Common
{
    public static class PredefinedUserData
    {
        public static readonly UserRegistrationModel GuestDataModel = new UserRegistrationModel
        {
            Email = "",
            Password = "",
            Gender = "",
            Role = "guest",
            Device = new DeviceData
            {
                platform = "android", deviceId = DeviceUtility.GetDeviceId(),
                deviceToken = DeviceUtility.GetDeviceToken()
            }
        };
    }
}