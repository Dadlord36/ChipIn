﻿using UnityEngine.Assertions;
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