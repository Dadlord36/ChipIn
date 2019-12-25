using DataModels.RequestsModels;
using Utilities;

namespace Common
{
    public static class PredefinedUserData
    {
        public static readonly RegistrationRequestModel GuestDataRequestModel = new RegistrationRequestModel
        {
            Email = "",
            Password = "",
            Gender = "",
            Role = "guest",
            Device = new DeviceData
            {
                Platform = "android", DeviceId = DeviceUtility.GetDeviceId(),
                DeviceToken = DeviceUtility.GetDeviceToken()
            }
        };
    }
}