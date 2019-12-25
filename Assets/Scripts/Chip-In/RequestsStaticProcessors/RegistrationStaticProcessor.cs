using System.Threading.Tasks;
using DataModels.RequestsModels;
using HttpRequests.RequestsProcessors.PostRequests;

namespace RequestsStaticProcessors
{
    public static class RegistrationStaticProcessor
    {
        public static async Task<bool> RegisterUserFull(
            RegistrationRequestModel registrationRequestModel)
        {
            var response =
                await new SimpleRegistrationRequestProcessor(registrationRequestModel).SendRequest(
                    "User have been registered successfully!");
            return response.ResponseModelInterface != null;
        }

        public static async Task<bool> RegisterUserSimple(SimpleRegistrationRequestModel registrationRequestModel)
        {
            var response =
                await new SimpleRegistrationRequestProcessor(registrationRequestModel).SendRequest(
                    "User have been registered successfully!");
            return response.ResponseModelInterface != null;
        }
    }
}