using DataModels.RequestsModels;
using Utilities;

namespace Common
{
    public static class PredefinedUserData
    {
        public static readonly RegistrationRequestModel GuestDataRequestModel = new RegistrationRequestModel
        {
            Email = null,
            Password = "",
            Gender = "",
            Role = "guest",
            Device = DeviceUtility.DeviceData
        };
    }
}