using DataModels;
using UnityEngine;
using Utilities;

namespace Common
{
    public static class PredefinedUserData
    {
        public static UserRegistrationModel guestDataModel = new UserRegistrationModel
        {
            Device = new DeviceData
            {
                platform = Application.platform.ToString(), deviceId = DeviceUtility.GetDeviceId(),
                deviceToken = DeviceUtility.GetDeviceToken()
            }
        };
    }
}