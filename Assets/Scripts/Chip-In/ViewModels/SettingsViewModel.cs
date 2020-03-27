using Controllers;
using UnityEngine;
using UnityWeld.Binding;
using Views.Cards.Settings;
using Views.ViewElements.ViewsPlacers;

namespace ViewModels
{
    [Binding]
    public sealed class SettingsViewModel : ViewsSwitchingViewModel
    {
        
        [SerializeField] private TwoSlotsViewsPlacer viewsPlacer;
        [SerializeField] private SessionController sessionController;

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
        public void LogOut_OnClick()
        {
            LogOut();
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

        private void LogOut()
        {
            sessionController.SignOut();
        }
    }
}