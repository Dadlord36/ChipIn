﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Common.Structures;
using DataModels;
using DataModels.Interfaces;
using Newtonsoft.Json;
using Repositories.Interfaces;
using ScriptableObjects.DataSynchronizers;
using UnityEngine;
using WebOperationUtilities;

namespace Repositories
{
    [CreateAssetMenu(fileName = nameof(UserProfileRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(UserProfileRemoteRepository), order = 0)]
    public sealed class UserProfileRemoteRepository : ScriptableObject, INotifyPropertyChanged, IUserProfileModel, IDataSynchronization
    {
        #region EventsDeclaration

        public event Action DataWasLoaded;
        public event Action DataWasSaved;

        #endregion

        [SerializeField] private UserProfileDataSynchronizer userProfileDataSynchronizer;
        
        private IUserProfileDataWebModel UserProfileDataRemote => userProfileDataSynchronizer;
        private IDataSynchronization UserProfileDataSynchronization => userProfileDataSynchronizer;

        [SerializeField] private Texture2D userAvatarImage;

        #region IUserProfile delegation
        public GeoLocation UserLocation
        {
            get => UserProfileDataRemote.UserLocation;
            set => UserProfileDataRemote.UserLocation = value;
        }

        public Texture2D AvatarImage
        {
            get => userAvatarImage;
            set => userAvatarImage = value;
        }

        public string Name
        {
            get => UserProfileDataRemote.Name;
            set => UserProfileDataRemote.Name = value;
        }

        public int Id
        {
            get => UserProfileDataRemote.Id;
            set => UserProfileDataRemote.Id = value;
        }

        public string Email
        {
            get => UserProfileDataRemote.Email;
            set => UserProfileDataRemote.Email = value;
        }

        public string Role
        {
            get => UserProfileDataRemote.Role;
            set => UserProfileDataRemote.Role = value;
        }

        public string Gender
        {
            get => UserProfileDataRemote.Gender;
            set => UserProfileDataRemote.Gender = value;
        }

        public string Birthday
        {
            get => UserProfileDataRemote.Birthday;
            set => UserProfileDataRemote.Birthday = value;
        }

        public string CountryCode
        {
            get => UserProfileDataRemote.CountryCode;
            set => UserProfileDataRemote.CountryCode = value;
        }

        public int TokensBalance
        {
            get => UserProfileDataRemote.TokensBalance;
            set => UserProfileDataRemote.TokensBalance = value;
        }

        public bool ShowAdsState
        {
            get => UserProfileDataRemote.ShowAdsState;
            set => UserProfileDataRemote.ShowAdsState = value;
        }

        public bool ShowAlertsState
        {
            get => UserProfileDataRemote.ShowAlertsState;
            set => UserProfileDataRemote.ShowAlertsState = value;
        }

        public bool UserRadarState
        {
            get => UserProfileDataRemote.UserRadarState;
            set => UserProfileDataRemote.UserRadarState = value;
        }

        public bool ShowNotificationsState
        {
            get => UserProfileDataRemote.ShowNotificationsState;
            set => UserProfileDataRemote.ShowNotificationsState = value;
        }

        #endregion

        private void OnEnable()
        {
            PropertyChanged += InvokeSaveDataToServer;
        }

        private void OnDestroy()
        {
            PropertyChanged -= InvokeSaveDataToServer;
        }

        private void InvokeSaveDataToServer(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            SaveDataToServer();
        }

        private async Task LoadAvatarImageFromServerAsync()
        {
            if (string.IsNullOrEmpty(UserProfileDataRemote.AvatarImageUrl))
            {
                Debug.Log("There is not URL to load user profile avatar image from",this);
                return;
            }
            userAvatarImage = await ImagesDownloadingUtility.DownloadImageAsync(UserProfileDataRemote.AvatarImageUrl);
            if (userAvatarImage)
                Debug.Log("User avatar image was loaded", this);
            else
            {
                Debug.Log("User avatar image is null after being loaded", this);
            }
        }

        public async Task LoadDataFromServer()
        {
            await UserProfileDataSynchronization.LoadDataFromServer();
            await LoadAvatarImageFromServerAsync();
            ConfirmDataLoading();
        }

        public async Task SaveDataToServer()
        {
            await UserProfileDataSynchronization.SaveDataToServer();
            ConfirmDataSaved();
        }

        private void ConfirmDataLoading()
        {
            OnDataWasLoaded();
            Debug.Log("User profile data was loaded from server", this);
            Debug.Log(JsonConvert.SerializeObject(UserProfileDataRemote), this);
        }

        private void ConfirmDataSaved()
        {
            OnDataWasSaved();
            Debug.Log("User profile data was saved to server", this);
        }

        #region EventsInvokation

        private void OnDataWasLoaded()
        {
            DataWasLoaded?.Invoke();
        }

        private void OnDataWasSaved()
        {
            DataWasSaved?.Invoke();
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => userProfileDataSynchronizer.PropertyChanged += value;
            remove => userProfileDataSynchronizer.PropertyChanged -= value;
        }
    }
}