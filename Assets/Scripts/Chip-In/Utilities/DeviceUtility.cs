using UnityEngine;

namespace Utilities
{
    public static class DeviceUtility
    {
        public static string GetDeviceToken()
        {
            return string.Empty;
        }

        public static string GetDeviceId()
        {
            return SystemInfo.deviceUniqueIdentifier;
        }
    }
}