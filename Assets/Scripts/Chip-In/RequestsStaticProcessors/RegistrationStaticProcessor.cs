using System;
using System.Threading.Tasks;
using DataModels;
using DataModels.RequestsModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.PostRequests;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class RegistrationStaticProcessor
    {
        public static async Task<BaseRequestProcessor<RegistrationRequestModel, RegistrationResponseDataModel, IRegistrationResponseDataModel>.HttpResponse> TryRegisterUserFull(
            RegistrationRequestModel registrationRequestModel)
        {
            try
            {
                return await new RegistrationRequestProcessor(registrationRequestModel).SendRequest(
                    "User have been registered successfully!");
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
                var response = await new SimpleRegistrationRequestProcessor(registrationRequestModel).SendRequest("User have been registered successfully!");
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