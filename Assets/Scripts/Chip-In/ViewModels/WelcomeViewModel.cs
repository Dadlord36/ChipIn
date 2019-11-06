using HumbleObjects;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public class WelcomeViewModel : BaseViewModel
    {
        [Binding]
        public void SwitchToLoginWindow()
        {
            viewsSwitchingBinding.SwitchView<LoginViewModel>(View);
        }

        [Binding]
        public async void LoginAsGuest()
        {
            await GuestRegistrationProcessor.RegisterUserAsGuest();
        }
    }
}