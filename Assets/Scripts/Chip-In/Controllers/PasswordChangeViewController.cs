using System;
using ViewModels.Elements;

namespace Controllers
{
    public class PasswordChangeViewController
    {
        public event Action<string> NewPasswordApproved
        {
            add => _passwordChangingViewModel.NewPasswordApproved += value;
            remove => _passwordChangingViewModel.NewPasswordApproved -= value;
        }

        private readonly IPasswordChangingViewModel _passwordChangingViewModel; 
        public PasswordChangeViewController(IPasswordChangingViewModel passwordChangingViewModel)
        {
            _passwordChangingViewModel = passwordChangingViewModel;
        }

        public void ShowUp()
        {
            
        }

        public void Hide()
        {
            
        }
    }
}