using DataModels.RequestsModels;

#if !UNITY_EDITOR || UNITY_STANDALONE
using UnityEngine;
#endif

namespace Utilities
{
    public static class DeviceUtility
    {
        public static IBaseDeviceData BaseDeviceData => DeviceData;

        public static DeviceData DeviceData => new DeviceData(DeviceId,
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_ANDROID
            "android",
#elif UNITY_IOS
            "ios"
#endif
            DeviceToken);

        //TODO: Implement getting of correct device token
        public static string DeviceToken { get; set; } = "m6aHiiHOc";

        public static string DeviceId
        {
#if UNITY_EDITOR
             get => "UVr864F8zUbyYOAUd4cFOW9hpsZuGn";
#else
             get=> SystemInfo.deviceUniqueIdentifier;
#endif
        }
    }
}