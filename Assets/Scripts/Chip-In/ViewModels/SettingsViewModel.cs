using System;
using System.Threading.Tasks;
using Controllers;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class SettingsViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private SessionController sessionController;
        
        public SettingsViewModel() : base(nameof(SettingsViewModel))
        {
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
        public void EditProfileButton_OnClick()
        {
            SwitchToView(nameof(EditProfileView));
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