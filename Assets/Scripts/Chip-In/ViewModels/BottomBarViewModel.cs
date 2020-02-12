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

        private string CurrentView => ((BottomBarView) View).CurrentViewName;
        
        [Binding]
        public void SwitchToMarketplaceView()
        {
            SwitchToView(nameof(MarketplaceView),CurrentView);
        }

        [Binding]
        public void SwitchToChallengesView()
        {
            SwitchToView(nameof(MyChallengeView),CurrentView);
        }

        [Binding]
        public void SwitchToCartView()
        {
            SwitchToView(nameof(CartView),CurrentView);
        }

        [Binding]
        public void SwitchToCommunityView()
        {
            SwitchToView(nameof(CommunityView),CurrentView);
        }

        [Binding]
        public void SwitchToSettingsView()
        {
            SwitchToView(nameof(SettingsView),CurrentView);
        }
    }
}