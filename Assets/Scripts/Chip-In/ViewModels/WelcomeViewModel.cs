using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using RequestsStaticProcessors;
using UnityWeld.Binding;
using Views;

namespace ViewModels
{
    [Binding]
    public sealed class WelcomeViewModel : ViewsSwitchingViewModel, INotifyPropertyChanged
    {
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
            bool success = await GuestRegistrationStaticProcessor.RegisterUserAsGuest();
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