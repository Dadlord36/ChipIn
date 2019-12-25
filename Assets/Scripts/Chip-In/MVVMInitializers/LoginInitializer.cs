using DataModels.RequestsModels;
using ViewModels;
using Views;

namespace MVVMInitializers
{
    public class LoginInitializer
    {
        private UserLoginRequestModel _loginRequestModel;

        public void Initialize(LoginView loginView, LoginViewModel loginViewModel)
        {
            _loginRequestModel = new UserLoginRequestModel();
            
            
            
        }
    }
}