using HumbleObjects;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class WelcomeViewModel : ViewsSwitchingViewModel
    {
        [Binding]
        public void SwitchToLoginWindow()
        {
            SwitchToView(nameof(LoginView));
        }

        [Binding]
        public async void LoginAsGuest()
        {
           bool success = await GuestRegistrationProcessor.RegisterUserAsGuest();
           if (success)
           {
               SwitchToView(nameof(MarketplaceView));
           }
        }
    }
}