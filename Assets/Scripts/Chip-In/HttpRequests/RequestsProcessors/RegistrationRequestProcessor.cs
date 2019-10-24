using DataModels;

namespace HttpRequests
{
    public class RegistrationRequestProcessor : PostRequestProcessor<UserRegistrationModel, UserProfileModel>
    {
        public RegistrationRequestProcessor() : base("sign_up")
        {
        }
    }
}