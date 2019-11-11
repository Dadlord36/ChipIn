using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class BottomBarViewModel : ViewsSwitchingViewModel
    {
        [Binding]
        public void SwitchToMarketplaceView()
        {
            SwitchToView(nameof(LoginView));
        }

        [Binding]
        public void SwitchToChallengeView()
        {
            SwitchToView(nameof(ChallengeView));
        }

        [Binding]
        public void SwitchToCartView()
        {
            SwitchToView(nameof(CartView));
        }

        [Binding]
        public void SwitchToCommunityView()
        {
            SwitchToView(nameof(CommunityView));
        }

        [Binding]
        public void SwitchToSettingsView()
        {
            SwitchToView(nameof(SettingsView));
        }
    }
}