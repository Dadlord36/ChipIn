using System.Threading.Tasks;
using DataModels;
using HttpRequests.RequestsProcessors;
using UnityEngine;
using Utilities.ApiExceptions;

namespace HumbleObjects
{
    public static class RegistrationProcessor
    {
        public static async Task<bool> RegisterUserFull(UserRegistrationModel registrationModel)
        {
            bool isSuccessful = false;
            try
            {
                var response = await new RegistrationRequestProcessor().SendRequest(registrationModel);
                if (response.responseData == null)
                {
                    Debug.LogError("Response data is equals null");
                }

                isSuccessful = response.responseMessage.IsSuccessStatusCode;
                
                Debug.Log(isSuccessful
                    ? "User have been registered successfully!"
                    : response.responseMessage.ReasonPhrase);
            }
            catch (ApiException e)
            {
                Debug.LogException(e);
            }

            return isSuccessful;
        }

        public static async Task<bool> RegisterUserSimple(UserSimpleRegistrationModel registrationModel)
        {
            bool isSuccessful = false;
            try
            {
                var response = await new SimpleRegistrationRequestProcessor().SendRequest(registrationModel);
                if (response.responseData == null)
                {
                    Debug.LogError("Response data is equals null");
                }

                isSuccessful = response.responseMessage.IsSuccessStatusCode;
                
                Debug.Log(isSuccessful
                    ? "User have been registered successfully!"
                    : response.responseMessage.ReasonPhrase);
            }
            catch (ApiException e)
            {
                Debug.LogException(e);
            }

            return isSuccessful;
        }
    }
}