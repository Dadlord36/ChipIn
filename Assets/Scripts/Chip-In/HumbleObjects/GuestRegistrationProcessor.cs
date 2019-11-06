using System.Threading.Tasks;
using Common;
using UnityEngine;

namespace HumbleObjects
{
    public static class GuestRegistrationProcessor
    {
        public static async Task<bool> RegisterUserAsGuest()
        {
            var registrationWasSuccessful =
                await RegistrationProcessor.RegisterUserFull(PredefinedUserData.guestDataModel);
            Debug.Log(registrationWasSuccessful ? "User was register as Guest" : "Failed to register user as Guest");

            return registrationWasSuccessful;
        }
    }
}