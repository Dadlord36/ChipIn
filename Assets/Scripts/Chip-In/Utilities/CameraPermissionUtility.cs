using System;
using pingak9;

namespace Utilities
{
    public static class CameraPermissionUtility
    {
        private const string Tag = nameof(CameraPermissionUtility);
        
        public static bool WentToAppSettings { get; set; }
        public static bool GoToSettingsPermissionRequested { get;  private set; }
        public static void RequestGoToSettingsPermission(Action onPermissionRejectedAction)
        {
            GoToSettingsPermissionRequested = true;
            NativeDialog.OpenDialog("Go To Settings", "Do you want to to app settings?", "Yes", "No",
                () =>
                {
                    if (NativeCamera.CanOpenSettings())
                    {
                        GoToSettingsPermissionRequested = false;
                        WentToAppSettings = true;
                        NativeCamera.OpenSettings();
                    }
                    else
                    {
                        GoToSettingsPermissionRequested = false;
                        LogUtility.PrintLog(Tag, "This Webcam library can't work without the webcam authorization");
                        onPermissionRejectedAction?.Invoke();
                    }
                }, onPermissionRejectedAction);
        }
        
        public static void AskForCameraPermissionAndActivateScanner(Action onPermissionRejectedAction, Action onPermissionGranted,
            Action onPermissionShouldAsk)
        {
            switch (NativeCamera.RequestPermission())
            {
                case NativeCamera.Permission.Denied:
                {
                    RequestGoToSettingsPermission(onPermissionRejectedAction);
                    break;
                }
                case NativeCamera.Permission.Granted:
                {
                    onPermissionGranted?.Invoke();
                    break;
                }
                case NativeCamera.Permission.ShouldAsk:
                {
                    onPermissionShouldAsk?.Invoke();
                    break;
                }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}