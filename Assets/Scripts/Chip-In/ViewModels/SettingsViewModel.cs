using System;
using Controllers;
using UnityEngine;
using UnityWeld.Binding;
using Views.Cards.Settings;

namespace ViewModels
{
    [Binding]
    public sealed class SettingsViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private CachingController cachingController;
        private void Start()
        {
            ShowMyProfile();
        }
        
        [Binding]
        public void LogOut_OnClick()
        {
            cachingController.ClearCache();
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