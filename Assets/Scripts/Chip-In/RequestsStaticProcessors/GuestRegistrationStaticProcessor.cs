using System.Threading.Tasks;
using Common;
using DataModels;
using DataModels.RequestsModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.PostRequests;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class GuestRegistrationStaticProcessor
    {
        private const string Tag = nameof(GuestRegistrationStaticProcessor);

        public static Task<BaseRequestProcessor<RegistrationRequestModel, RegistrationResponseDataModel, 
            IRegistrationResponseDataModel>.HttpResponse> TryRegisterUserAsGuest()
        {
            LogUtility.PrintLog(Tag, "Trying to login as guest");
            return RegistrationStaticProcessor.TryRegisterUserFull(PredefinedUserData.GuestDataRequestModel);
        }
    }
}