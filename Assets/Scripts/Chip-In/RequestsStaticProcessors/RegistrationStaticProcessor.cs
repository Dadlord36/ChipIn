using System;
using System.Threading.Tasks;
using DataModels.RequestsModels;
using HttpRequests.RequestsProcessors.PostRequests;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class RegistrationStaticProcessor
    {
        public static async Task<bool> TryRegisterUserFull(
            RegistrationRequestModel registrationRequestModel)
        {
            try
            {
                var response =
                    await new SimpleRegistrationRequestProcessor(registrationRequestModel).SendRequest(
                        "User have been registered successfully!");
                return response.ResponseModelInterface != null;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<bool> TryRegisterUserSimple(SimpleRegistrationRequestModel registrationRequestModel)
        {
            try
            {
                var response =
                    await new SimpleRegistrationRequestProcessor(registrationRequestModel).SendRequest(
                        "User have been registered successfully!");
                return response.ResponseModelInterface != null;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}