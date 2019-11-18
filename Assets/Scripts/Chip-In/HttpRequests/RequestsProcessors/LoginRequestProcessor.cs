using DataModels;

namespace HttpRequests.RequestsProcessors
{
    public class LoginRequestProcessor : PostRequestProcessor<UserLoginModel,LoginResponseModel>
    {
        public LoginRequestProcessor() : base("sign_in")
        {
        }
    }
}