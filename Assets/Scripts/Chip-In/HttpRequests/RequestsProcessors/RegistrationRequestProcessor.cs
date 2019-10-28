using DataModels;

namespace HttpRequests.RequestsProcessors
{
    public class RegistrationRequestProcessor : PostRequestProcessor<UserSimpleRegistrationModel, UserProfileModel>
    {
        public RegistrationRequestProcessor() : base("sign_up")
        {
        }
    }
}