using System.Threading.Tasks;
using UnityEngine;

namespace RunTimeTests.Common
{
    public static class AsyncRegistrationHelper
    {
        /*public static async Task<bool> TryToRegister(SimpleRegistrationRequestModel registrationRequestModel)
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
        }*/
    }
}