using System;
using HttpRequests;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels
{
    [Binding]
    public class WelcomeViewModel : BaseViewModel
    {
        [Binding]
        public void SwitchToLoginWindow()
        {
            viewsSwitchingBinding.SwitchView<LoginViewModel>(View);
        }

        [Binding]
        public async void LoginAsGuest()
        {
            try
            {
                await GuestRegistration.Register();
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}