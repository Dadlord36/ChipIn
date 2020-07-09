﻿using System.ComponentModel;
using Common.Structures;
using DataModels.Interfaces;
using Repositories.Local;
using Repositories.Remote;
using UnityEngine;
using UnityWeld.Binding;
using Utilities;
using ViewModels.Basic;
using Views.ViewElements;

namespace ViewModels.Settings
{
    [Binding]
    public class UserProfileViewModel : BaseViewModel, INotifyPropertyChanged
    {
        [SerializeField] private UserProfileRemoteRepository repository;
        [SerializeField] private GeoLocationRepository geoLocationRepository;

        [SerializeField] private SimpleView passwordChangingView;

        private IUserProfileModel UserSettingsModel => repository;

        #region UserProfileModel interface implementation

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
            get => UserSettingsModel.Id ?? 0;
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

        #endregion

        public UserProfileViewModel() : base(nameof(UserProfileViewModel))
        {
        }


        [Binding]
        public void ChangePassword_Click()
        {
            ShowPasswordSwitchingView();
            LogUtility.PrintLog(Tag, "Changing password");
        }

        [Binding]
        public void EditProfile_Click()
        {
            LogUtility.PrintLog(Tag, "Editing profile");
        }

        [Binding]
        public void UserRadar_OnTryToTurnOn()
        {
            geoLocationRepository.SetLocationServiceActivity(!UserRadarState);
        }

        private void ShowPasswordSwitchingView()
        {
            passwordChangingView.Show();
        }


        public event PropertyChangedEventHandler PropertyChanged
        {
            add => repository.PropertyChanged += value;
            remove => repository.PropertyChanged -= value;
        }
    }
}