using System;
using System.Threading.Tasks;
using Common;
using DataModels;
using Newtonsoft.Json;
using UnityEngine;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class GuestRegistrationStaticProcessor
    {
        public static async Task<IAuthorisationModel> TryRegisterUserAsGuest()
        {
            try
            {
                Debug.Log($"Registration Request Model Data: {JsonConvert.SerializeObject(PredefinedUserData.GuestDataRequestModel)}");
                Debug.Log("Trying to login as guest");
                var registrationResponse = await RegistrationStaticProcessor.TryRegisterUserFull(PredefinedUserData.GuestDataRequestModel);
                Debug.Log(registrationResponse.Success ? "User was register as Guest" : "Failed to register user as Guest");
                return registrationResponse.AuthorisationData;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}