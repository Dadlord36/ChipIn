using DataModels;
using ViewModels;
using Views;

namespace MVVMInitializers
{
    public class LoginInitializer
    {
        private UserLoginModel _loginModel;

        public void Initialize(LoginView loginView, LoginViewModel loginViewModel)
        {
            _loginModel = new UserLoginModel();
            
            
            
        }
    }
}