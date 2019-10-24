using DataModels;

namespace HttpRequests
{
    public class LoginRequestProcessor : PostRequestProcessor<UserLoginModel,LoginResponseModel>
    {
        public LoginRequestProcessor() : base("sign_in")
        {
        }
    }
}