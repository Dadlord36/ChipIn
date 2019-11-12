using UnityEngine.Assertions;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public class BottomBarViewModel : ViewsSwitchingViewModel
    {
        private BottomBarView _bottomBarView;

        private void Awake()
        {
            _bottomBarView = View as BottomBarView;
            Assert.IsNotNull(_bottomBarView);
           
        }

        protected override void Start()
        {
            base.Start();
//            SubscribeOnViewSwitched(_bottomBarView.ChangeViewActivityBasedOnCurrentViewName);
        }


        [Binding]
        public void SwitchToMarketplaceView()
        {
            SwitchToView(nameof(MarketplaceView));
        }

        [Binding]
        public void SwitchToChallengesView()
        {
            SwitchToView(nameof(ChallengesView));
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