using DataModels;
using DefaultNamespace.GlobalVarialbles;

namespace HttpRequests.RequestsProcessors
{
    public class RegistrationRequestProcessor : PostRequestProcessor<UserRegistrationModel, UserProfileModel>
    {
        public RegistrationRequestProcessor() : base(RequestsSuffixes.SignUp)
        {
        }
    }

    public class
        SimpleRegistrationRequestProcessor : PostRequestProcessor<UserSimpleRegistrationModel, UserProfileModel>
    {
        public SimpleRegistrationRequestProcessor() : base(RequestsSuffixes.SignUp)
        {
        }
    }
}