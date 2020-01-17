using System;
using UnityWeld.Binding;
using Views.Cards.Settings;

namespace ViewModels
{
    [Binding]
    public sealed class SettingsViewModel : ViewsSwitchingViewModel
    {
        private void Start()
        {
            ShowMyProfile();
        }

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
    }
}