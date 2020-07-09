using Firebase.Messaging;
using Utilities;

namespace Notifications
{
    public static class FireBaseNotificationsController
    {
        private const string Tag = nameof(FireBaseNotificationsController);
        
        public static string Token {  get; private set; }
        
        public static void Initialize()
        {
            FirebaseMessaging.TokenReceived += FirebaseMessagingOnTokenReceived;
            FirebaseMessaging.MessageReceived += FirebaseMessagingOnMessageReceived;
        }

        public static void Dispose()
        {
            FirebaseMessaging.TokenReceived -= FirebaseMessagingOnTokenReceived;
            FirebaseMessaging.MessageReceived -= FirebaseMessagingOnMessageReceived;
        }

        
        private static void FirebaseMessagingOnMessageReceived(object sender, MessageReceivedEventArgs e)
        {
            LogUtility.PrintLog(Tag, $"Received FireBase message: {e.Message}");
        }


        private static void FirebaseMessagingOnTokenReceived(object sender, TokenReceivedEventArgs e)
        {
            Token = e.Token;
            LogUtility.PrintLog(Tag, $"Received FireBase Token: {e.Token}");
        }
    }
}