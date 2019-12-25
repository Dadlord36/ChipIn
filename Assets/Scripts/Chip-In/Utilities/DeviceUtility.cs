#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace Utilities
{
    public static class DeviceUtility
    {
        public static string GetDeviceToken()
        {
            //ToDo: Implement correct device Token Getter
            return "m6aHiiHOc";
        }

        public static string GetDeviceId()
        {
#if UNITY_EDITOR
            return "UVr864F8zUbyYOAUd4cFOW9hpsZuGn";
#else
            return SystemInfo.deviceUniqueIdentifier;
#endif
        }
    }
}