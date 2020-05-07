using System.ComponentModel;
using System.Runtime.CompilerServices;
using Controllers;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;
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
            
            bool success = await sessionController.TryRegisterAndLoginAsGuest();
                
            IsPendingLogin = false;

            if (success)
            {
                SwitchToView(nameof(CoinsGameView));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}