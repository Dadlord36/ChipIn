using System.Threading.Tasks;
using DataModels;
using DataModels.RequestsModels;
using HttpRequests;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities.ApiExceptions;

namespace RunTimeTests.Common
{
    public static class AsyncRegistrationHelper
    {
        public static async Task<bool> TryToRegister(SimpleRegistrationRequestModel registrationRequestModel)
        {
            ApiHelper.InitializeClient();

            bool successful = false;
            try
            {
                successful = await RegistrationStaticProcessor.RegisterUserSimple(registrationRequestModel);
            }
            catch (ApiException e)
            {
                Debug.Log(e);
            }

            ApiHelper.Dispose();

            return successful;
        }
    }
}