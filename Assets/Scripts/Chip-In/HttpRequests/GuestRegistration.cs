using System;
using System.Threading.Tasks;
using DataModels;
using HttpRequests.RequestsProcessors;

using UnityEngine;

namespace HttpRequests
{
    public static class GuestRegistration
    {
        public static async Task<bool> Register()
        {
            var guestRegistrationDataModel = new UserRegistrationModel
            {
                Role = "guest",
                Device = new DeviceData(Application.platform.ToString(), SystemInfo.deviceUniqueIdentifier, "")
            };
            try
            {
                var response = await new RegistrationRequestProcessor().SendRequest(guestRegistrationDataModel);
                Debug.Log(response.responseMessage.IsSuccessStatusCode
                    ? "Logged in as guest"
                    : response.responseMessage.ReasonPhrase);
                return response.responseMessage.IsSuccessStatusCode;
            }
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }
        }
    }
}