using System;
using UnityEngine;
using UnityWeld.Binding;
using Views.Settings;

namespace ViewModels
{
    [Binding]
    public sealed class SettingsViewModel : ViewsSwitchingViewModel
    {
        [Binding]
        public void ShowMyProfile()
        {
            SwitchToView(nameof(UserProfileView));
        }

        [Binding]
        public void ShowMyWallet()
        {
            SwitchToView(nameof(TokenBalanceView));
        }

        [Binding]
        public void ShowMyInterest()
        {
        }

        protected override void Start()
        {
            base.Start();
            if (!Application.isPlaying) return;
            ShowMyProfile();
        }
    }
}