using DataModels;

namespace HttpRequests.RequestsProcessors
{
    public class RegistrationRequestProcessor : PostRequestProcessor<UserRegistrationModel, UserProfileModel>
    {
        public RegistrationRequestProcessor() : base("sign_up")
        {
        }
    }
}