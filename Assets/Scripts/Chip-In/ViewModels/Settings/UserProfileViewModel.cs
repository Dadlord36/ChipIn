using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Structures;
using DataModels.Interfaces;
using JetBrains.Annotations;
using Repositories;
using UnityEngine;
using UnityWeld.Binding;

namespace ViewModels.Settings
{
    [Binding]
    public class UserProfileViewModel : BaseViewModel, INotifyPropertyChanged, IUserProfileModel
    {
        public event PropertyChangedEventHandler PropertyChanged;

        [SerializeField] private UserProfileRemoteRepository repository;
        private IUserProfileModel UserSettingsModel => repository;

        [Binding]
        public GeoLocation UserLocation
        {
            get => UserSettingsModel.UserLocation;
            set
            {
                UserSettingsModel.UserLocation = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public Texture2D AvatarImage
        {
            get => UserSettingsModel.AvatarImage;
            set
            {
                UserSettingsModel.AvatarImage = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Name
        {
            get => UserSettingsModel.Name;
            set
            {
                UserSettingsModel.Name = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int Id
        {
            get => UserSettingsModel.Id;
            set
            {
                UserSettingsModel.Id = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Email
        {
            get => UserSettingsModel.Email;
            set
            {
                UserSettingsModel.Email = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Role
        {
            get => UserSettingsModel.Role;
            set
            {
                UserSettingsModel.Role = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Gender
        {
            get => UserSettingsModel.Gender;
            set
            {
                UserSettingsModel.Gender = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string Birthday
        {
            get => UserSettingsModel.Birthday;
            set
            {
                UserSettingsModel.Birthday = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public string CountryCode
        {
            get => UserSettingsModel.CountryCode;
            set
            {
                UserSettingsModel.CountryCode = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public int TokensBalance
        {
            get => UserSettingsModel.TokensBalance;
            set
            {
                UserSettingsModel.TokensBalance = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool ShowAdsState
        {
            get => UserSettingsModel.ShowAdsState;
            set
            {
                UserSettingsModel.ShowAdsState = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool ShowAlertsState
        {
            get => UserSettingsModel.ShowAlertsState;
            set
            {
                UserSettingsModel.ShowAlertsState = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool UserRadarState
        {
            get => UserSettingsModel.UserRadarState;
            set
            {
                UserSettingsModel.UserRadarState = value;
                OnPropertyChanged();
            }
        }

        [Binding]
        public bool ShowNotificationsState
        {
            get => UserSettingsModel.ShowNotificationsState;
            set
            {
                UserSettingsModel.ShowNotificationsState = value;
                OnPropertyChanged();
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

        protected override void OnEnable()
        {
            base.OnEnable();
        }


        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Debug.Log($"{propertyName} was changed");
           //await seems to be not needed
            repository.SaveDataToServer();
        }
    }
}