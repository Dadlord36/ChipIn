using UnityEngine.Assertions;
using UnityWeld.Binding;
using Views;
using Views.Bars;

namespace ViewModels
{
    [Binding]
    public class BottomBarViewModel : ViewsSwitchingViewModel
    {
        private void Awake()
        {
            Assert.IsNotNull(View as BottomBarView);
        }

        [Binding]
        public void SwitchToMarketplaceView()
        {
            SwitchToView(nameof(MarketplaceView));
        }

        [Binding]
        public void SwitchToChallengesView()
        {
            SwitchToView(nameof(MyChallengeView));
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