﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Common.Structures;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using Repositories;
using Repositories.Interfaces;
using RequestsStaticProcessors;
using UnityEngine;

namespace ScriptableObjects.DataSynchronizers
{
    [CreateAssetMenu(fileName = nameof(UserProfileDataSynchronizer),
        menuName = nameof(DataSynchronizers) + "/" + nameof(UserProfileDataSynchronizer), order = 0)]
    public class UserProfileDataSynchronizer : ScriptableObject, IUserProfileDataWebModel, IDataSynchronization,
        INotifyPropertyChanged
    {
        #region EventsDeclaretion

        public event Action DataWasLoaded;
        public event Action DataWasSaved;

        #endregion

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        [SerializeField] private UserProfileDataWebModel userProfileData;

        private IUserProfileDataWebModel UserProfile => userProfileData;
        private IRequestHeaders RequestHeaders => authorisationDataRepository;

        public void Set(IUserProfileDataWebModel source)
        {
            UserProfile.Set(source);
        }

        public int Id
        {
            get => UserProfile.Id;
            set => UserProfile.Id = value;
        }

        public string Email
        {
            get => UserProfile.Email;
            set => UserProfile.Email = value;
        }

        public string Name
        {
            get => UserProfile.Name;
            set => UserProfile.Name = value;
        }

        public string Role
        {
            get => UserProfile.Role;
            set => UserProfile.Role = value;
        }

        public int TokensBalance
        {
            get => UserProfile.TokensBalance;
            set => UserProfile.TokensBalance = value;
        }

        public string Gender
        {
            get => UserProfile.Gender;
            set => UserProfile.Gender = value;
        }

        public bool ShowAdsState
        {
            get => UserProfile.ShowAdsState;
            set => UserProfile.ShowAdsState = value;
        }

        public bool ShowAlertsState
        {
            get => UserProfile.ShowAlertsState;
            set => UserProfile.ShowAlertsState = value;
        }

        public bool UserRadarState
        {
            get => UserProfile.UserRadarState;
            set => UserProfile.UserRadarState = value;
        }

        public bool ShowNotificationsState
        {
            get => UserProfile.ShowNotificationsState;
            set => UserProfile.ShowNotificationsState = value;
        }

        public GeoLocation UserLocation
        {
            get => UserProfile.UserLocation;
            set => UserProfile.UserLocation = value;
        }

        public string AvatarImageUrl
        {
            get => UserProfile.AvatarImageUrl;
            set => UserProfile.AvatarImageUrl = value;
        }

        public string Birthday
        {
            get => UserProfile.Birthday;
            set => UserProfile.Birthday = value;
        }

        public string CountryCode
        {
            get => UserProfile.CountryCode;
            set => UserProfile.CountryCode = value;
        }

        public async Task LoadDataFromServer()
        {
            var response = await UserProfileDataStaticRequestsProcessor.GetUserProfileData(RequestHeaders);
            UserProfile.Set(response.ResponseModelInterface);
            ConfirmDataLoading();
        }

        public async Task SaveDataToServer()
        {
            await UserProfileDataStaticRequestsProcessor.UpdateUserProfileData(RequestHeaders, UserProfile);
            ConfirmDataSaving();
        }

        private void ConfirmDataLoading()
        {
            OnDataWasLoaded();
        }

        private void ConfirmDataSaving()
        {
            OnDataWasSaved();
        }

        #region EventsInvokations

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
            add => userProfileData.PropertyChanged += value;
            remove => userProfileData.PropertyChanged -= value;
        }
    }
}