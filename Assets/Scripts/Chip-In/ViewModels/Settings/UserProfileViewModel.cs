using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Settings
{
    public struct UserProfileModel
    {
        public bool allowIncomingAds;
        public bool showAlerts;
        public bool showNotifications;
        public bool userRadar;
    }

    [Binding]
    public class UserProfileViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private UserProfileModel _userSettings;

        [Binding]
        public bool AllowIncomingAds
        {
            get => _userSettings.allowIncomingAds;
            set
            {
                _userSettings.allowIncomingAds = value;
                OnPropertyChanged(nameof(AllowIncomingAds));
            }
        }

        [Binding]
        public bool ShowAlerts
        {
            get => _userSettings.showAlerts;
            set
            {
                _userSettings.showAlerts = value;
                OnPropertyChanged(nameof(ShowAlerts));
            }
        }

        [Binding]
        public bool ShowNotification
        {
            get => _userSettings.showNotifications;
            set
            {
                _userSettings.showNotifications = value;
                OnPropertyChanged(nameof(ShowNotification));
            }
        }

        [Binding]
        public bool UserRadar
        {
            get => _userSettings.userRadar;
            set
            {
                _userSettings.userRadar = value;
                OnPropertyChanged(nameof(UserRadar));
            }
        }

        [Binding]
        public void ChangePassword_Click()
        {
            Debug.Log("Changing password");
        }
        
        [Binding]
        public void EditProfile_Click()
        {
            Debug.Log("Editing profile");   
        }

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Debug.Log($"{propertyName} was changed");
        }
    }
}