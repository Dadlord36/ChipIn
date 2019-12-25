using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using UnityEngine;

namespace RequestsStaticProcessors
{
    public static class GuestRegistrationStaticProcessor
    {
        public static async Task<bool> RegisterUserAsGuest()
        {
            Debug.Log("Registration Request Model Data: " + JsonConvert.SerializeObject(PredefinedUserData.GuestDataRequestModel));
            Debug.Log("Trying to login as guest");
            var registrationWasSuccessful =
                await RegistrationStaticProcessor.RegisterUserFull(PredefinedUserData.GuestDataRequestModel);
            Debug.Log(registrationWasSuccessful ? "User was register as Guest" : "Failed to register user as Guest");

            return registrationWasSuccessful;
        }
    }
}