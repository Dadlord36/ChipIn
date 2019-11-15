using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using UnityEngine;

namespace HumbleObjects
{
    public static class GuestRegistrationProcessor
    {
        public static async Task<bool> RegisterUserAsGuest()
        {
            Debug.Log(JsonConvert.SerializeObject(PredefinedUserData.GuestDataModel));
            Debug.Log("Trying to login as guest");
            var registrationWasSuccessful =
                await RegistrationProcessor.RegisterUserFull(PredefinedUserData.GuestDataModel);
            Debug.Log(registrationWasSuccessful ? "User was register as Guest" : "Failed to register user as Guest");

            return registrationWasSuccessful;
        }
    }
}