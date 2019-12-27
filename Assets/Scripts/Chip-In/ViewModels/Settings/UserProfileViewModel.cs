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
        [SerializeField] private UserProfileRemoteRepository repository;
        private IUserProfileModel UserSettingsModel => repository;

        [Binding]
        public GeoLocation UserLocation
        {
            get => UserSettingsModel.UserLocation;
            set => UserSettingsModel.UserLocation = value;
        }

        [Binding]
        public Texture2D AvatarImage
        {
            get => UserSettingsModel.AvatarImage;
            set => UserSettingsModel.AvatarImage = value;
        }

        [Binding]
        public string Name
        {
            get => UserSettingsModel.Name;
            set => UserSettingsModel.Name = value;
        }

        [Binding]
        public int Id
        {
            get => UserSettingsModel.Id;
            set => UserSettingsModel.Id = value;
        }

        [Binding]
        public string Email
        {
            get => UserSettingsModel.Email;
            set => UserSettingsModel.Email = value;
        }

        [Binding]
        public string Role
        {
            get => UserSettingsModel.Role;
            set => UserSettingsModel.Role = value;
        }

        [Binding]
        public string Gender
        {
            get => UserSettingsModel.Gender;
            set => UserSettingsModel.Gender = value;
        }

        [Binding]
        public string Birthday
        {
            get => UserSettingsModel.Birthday;
            set => UserSettingsModel.Birthday = value;
        }

        [Binding]
        public string CountryCode
        {
            get => UserSettingsModel.CountryCode;
            set => UserSettingsModel.CountryCode = value;
        }

        [Binding]
        public int TokensBalance
        {
            get => UserSettingsModel.TokensBalance;
            set => UserSettingsModel.TokensBalance = value;
        }

        [Binding]
        public bool ShowAdsState
        {
            get => UserSettingsModel.ShowAdsState;
            set => UserSettingsModel.ShowAdsState = value;
        }

        [Binding]
        public bool ShowAlertsState
        {
            get => UserSettingsModel.ShowAlertsState;
            set => UserSettingsModel.ShowAlertsState = value;
        }

        [Binding]
        public bool UserRadarState
        {
            get => UserSettingsModel.UserRadarState;
            set => UserSettingsModel.UserRadarState = value;
        }

        [Binding]
        public bool ShowNotificationsState
        {
            get => UserSettingsModel.ShowNotificationsState;
            set => UserSettingsModel.ShowNotificationsState = value;
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


        /*[NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            Debug.Log($"{propertyName} was changed");
           //await seems to be not needed
            repository.SaveDataToServer();
        }*/
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => repository.RepositoryPropertyChanged += value;
            remove => repository.RepositoryPropertyChanged -= value;
        }
    }
}