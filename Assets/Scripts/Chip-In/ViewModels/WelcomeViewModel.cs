using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Controllers;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class WelcomeViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
        [SerializeField] private SessionController sessionController;

        private bool _isPendingLogin;

        [Binding]
        public bool IsPendingLogin
        {
            get => _isPendingLogin;
            private set
            {
                if (value == _isPendingLogin) return;
                _isPendingLogin = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public void SwitchToLoginWindow()
        {
            SwitchToView(nameof(LoginView));
        }


        [Binding]
        public async void LoginAsGuest()
        {
            IsPendingLogin = true;

            try
            {
                bool success = await sessionController.TryRegisterAndLoginAsGuest();
                if (success)
                {
                    SwitchToView(nameof(CoinsGameView));
                }
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }

            IsPendingLogin = false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}