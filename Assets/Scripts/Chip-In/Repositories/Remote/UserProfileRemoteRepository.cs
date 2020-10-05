using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Common.Structures;
using Controllers;
using DataModels;
using DataModels.Extensions;
using DataModels.Interfaces;
using Factories.ReferencesContainers;
using Newtonsoft.Json;
using Repositories.Local;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;

namespace Repositories.Remote
{
    public interface IUserProfileRemoteRepository : IUserProfileModel, IClearable, INotifyPropertyChanged
    {
        Sprite UserAvatarSprite { get; set; }
        bool DataIsLoaded { get; }
        void SubscribeOnEvents();
        void UnsubscribeFromEvents();
        Task LoadDataFromServer();
        event Action DataWasLoaded;
        event Action DataWasSaved;
        void OnDataWasLoaded();
        void OnDataWasSaved();
    }

    public class UserProfileRemoteRepository : RemoteRepositoryBase, IUserProfileRemoteRepository
    {
        public UserProfileRemoteRepository() : base(nameof(UserProfileRemoteRepository))
        {
        }

        private UserProfileDataModel _userProfileDataModel = new UserProfileDataModel();
        private IUserProfileModel UserProfileDataRemote => _userProfileDataModel;

        private bool _isLoadingData;

        #region IUserProfile delegation

        private readonly AsyncOperationCancellationController _cancellationController = new AsyncOperationCancellationController();
        private Sprite _userAvatarSprite;

        public Sprite UserAvatarSprite
        {
            get => _userAvatarSprite;
            set
            {
                _userAvatarSprite = value;
                _userProfileDataModel.OnPropertyChanged();
            }
        }

        public GeoLocation UserLocation
        {
            get => UserProfileDataRemote.UserLocation;
            set => UserProfileDataRemote.UserLocation = value;
        }

        public string Avatar
        {
            get => UserProfileDataRemote.Avatar;
            set => UserProfileDataRemote.Avatar = value;
        }

        public string Name
        {
            get => UserProfileDataRemote.Name;
            set => UserProfileDataRemote.Name = value;
        }

        public int? Id
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

        public string CurrencyCode
        {
            get => UserProfileDataRemote.CurrencyCode;
            set => UserProfileDataRemote.CurrencyCode = value;
        }

        #endregion

        private void SetRadarActivityState(bool value)
        {
            UserRadarState = value;
        }

        public void SubscribeOnEvents()
        {
            DataRepositoriesReferencesContainer.GetObjectInstance<IGeoLocationRepository>().LocationServiceActivityChanged += SetRadarActivityState;
        }

        public void UnsubscribeFromEvents()
        {
            DataRepositoriesReferencesContainer.GetObjectInstance<IGeoLocationRepository>().LocationServiceActivityChanged -= SetRadarActivityState;
        }

        public override async Task LoadDataFromServer()
        {
            _isLoadingData = true;
            _cancellationController.CancelOngoingTask();

            var response = await ProfileDataStaticRequestsProcessor.GetUserProfileData(out _cancellationController.TasksCancellationTokenSource,
                AuthorisationDataRepository).ConfigureAwait(true);
            if (!response.Success) return;

            var responseInterface = response.ResponseModelInterface;

            UserProfileDataRemote.Set(responseInterface);
            UserAvatarSprite = await MainObjectsReferencesContainer.GetObjectInstance<IDownloadedSpritesRepository>()
                .CreateLoadSpriteTask(Avatar, _cancellationController.CancellationToken)
                .ConfigureAwait(false);
        }

        protected override void ConfirmDataLoading()
        {
            base.ConfirmDataLoading();
            _isLoadingData = false;
            LogUtility.PrintLog(Tag, $"User profile data was loaded from server: {JsonConvert.SerializeObject(UserProfileDataRemote)}");
        }

        protected override void ConfirmDataSaved()
        {
            base.ConfirmDataSaved();
            LogUtility.PrintLog(Tag, "User profile data was saved to server");
        }

        void IClearable.Clear()
        {
            _userProfileDataModel = new UserProfileDataModel();
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => _userProfileDataModel.PropertyChanged += value;
            remove => _userProfileDataModel.PropertyChanged -= value;
        }
    }
}