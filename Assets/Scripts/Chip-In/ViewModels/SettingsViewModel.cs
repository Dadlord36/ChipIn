using System;
using System.Threading.Tasks;
using Controllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views.Cards.Settings;
using Views.ViewElements.ViewsPlacers;

namespace ViewModels
{
    [Binding]
    public sealed class SettingsViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private TwoSlotsViewsPlacer viewsPlacer;
        [SerializeField] private SessionController sessionController;


        public SettingsViewModel() : base(nameof(SettingsViewModel))
        {
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
            Destroy(gameObject);
        }

        private void Start()
        {
            viewsPlacer.Initialize();
            ShowMyProfile();
        }

        [Binding]
        public async void LogOut_OnClick()
        {
            try
            {
                await LogOut();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
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
            SwitchToView(nameof(MyInterestView));
        }

        private Task LogOut()
        {
            return sessionController.SignOut();
        }
    }
}