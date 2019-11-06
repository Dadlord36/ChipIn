using System.Threading.Tasks;
using DataModels;
using HttpRequests;
using HumbleObjects;
using UnityEngine;
using Utilities.ApiExceptions;

namespace RunTimeTests.Common
{
    public static class AsyncRegistrationHelper
    {
        public static async Task<bool> TryToRegister(UserSimpleRegistrationModel userRegistrationModel)
        {
            ApiHelper.InitializeClient();

            bool successful = false;
            try
            {
                successful = await RegistrationProcessor.RegisterUserSimple(userRegistrationModel);
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