using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.RequestsModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.PostRequests;

namespace RequestsStaticProcessors
{
    public static class RegistrationStaticProcessor
    {
        public static Task<BaseRequestProcessor<RegistrationRequestModel, RegistrationResponseDataModel, IRegistrationResponseDataModel>.HttpResponse> TryRegisterUserFull(
            out DisposableCancellationTokenSource cancellationTokenSource, RegistrationRequestModel registrationRequestModel)
        {
            return new RegistrationRequestProcessor(out cancellationTokenSource, registrationRequestModel).SendRequest(
                "User have been registered successfully!");
        }

        public static
            Task<BaseRequestProcessor<SimpleRegistrationRequestModel, UserProfileDataWebModel, IUserProfileDataWebModel>.HttpResponse>
            TryRegisterUserSimple(out DisposableCancellationTokenSource cancellationTokenSource, SimpleRegistrationRequestModel registrationRequestModel)
        {
            return new SimpleRegistrationRequestProcessor(out cancellationTokenSource, registrationRequestModel).SendRequest(
                "User have been registered successfully!");
        }
    }
}