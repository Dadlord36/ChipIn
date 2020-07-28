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
            ShowMyProfile();
        }

        [Binding]
        public async void LogOut_OnClick()
        {
            try
            {
                await LogOut().ConfigureAwait(true);
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
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
           
        }

        [Binding]
        public void ShowMyWallet()
        {
           
        }

        [Binding]
        public void ShowMyInterest()
        {
            
        }

        private Task LogOut()
        {
            return sessionController.SignOut();
        }
    }
}