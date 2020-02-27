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
        private const string Tag = nameof(GuestRegistrationStaticProcessor);
        public static async Task<IAuthorisationModel> TryRegisterUserAsGuest()
        {
            try
            {
                LogUtility.PrintLog(Tag,$"Registration Request Model Data: {JsonConvert.SerializeObject(PredefinedUserData.GuestDataRequestModel)}");
                LogUtility.PrintLog(Tag,"Trying to login as guest");
                var registrationResponse = await RegistrationStaticProcessor.TryRegisterUserFull(PredefinedUserData.GuestDataRequestModel);
                LogUtility.PrintLog(Tag,registrationResponse.Success ? "User was register as Guest" : "Failed to register user as Guest");
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