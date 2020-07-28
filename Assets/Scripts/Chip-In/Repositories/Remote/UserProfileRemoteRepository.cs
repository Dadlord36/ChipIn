using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Common.Structures;
using Controllers;
using DataModels;
using DataModels.Interfaces;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Repositories.Local;
using ScriptableObjects.DataSynchronizers;
using UnityEngine;
using Utilities;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(UserProfileRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserProfileRemoteRepository), order = 0)]
    public sealed class UserProfileRemoteRepository : RemoteRepositoryBase, IUserProfileModel, IClearable,
        INotifyPropertyChanged
    {
        private const string Tag = nameof(UserProfileRemoteRepository);

        [SerializeField] private DownloadedSpritesRepository downloadedSpritesRepository;
        [SerializeField] private UserProfileDataSynchronizer userProfileDataSynchronizer;
        [SerializeField] private SessionStateRepository sessionStateRepository;
        [SerializeField] private GeoLocationRepository geoLocationRepository;

        private IUserProfileDataWebModel UserProfileDataRemote => userProfileDataSynchronizer;
        private IDataSynchronization UserProfileDataSynchronization => userProfileDataSynchronizer;
        private ILoginState SessionState => sessionStateRepository;

        private bool IsAllowedToSaveToServer => !_isLoadingData && SessionState.IsLoggedIn;

        [SerializeField] private Texture2D defaultAvatarImage;

        private bool _isLoadingData;

        #region IUserProfile delegation

        private readonly AsyncOperationCancellationController _cancellationController = new AsyncOperationCancellationController();

        public GeoLocation UserLocation
        {
            get => UserProfileDataRemote.UserLocation;
            set => UserProfileDataRemote.UserLocation = value;
        }

        public Texture2D AvatarImage
        {
            get => UserProfileDataRemote.AvatarImage ? UserProfileDataRemote.AvatarImage : defaultAvatarImage;
            set => UserProfileDataRemote.AvatarImage = value;
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

        #endregion

        private void SetRadarActivityState(bool value)
        {
            UserRadarState = value;
        }

        private void OnEnable()
        {
            BindToSynchronizerUpdatingEvent();
            PropertyChanged += OnProfileDataChanged;
            geoLocationRepository.LocationServiceActivityChanged += SetRadarActivityState;
        }


        private void OnDisable()
        {
            UnbindFromSynchronizerUpdatingEvent();
            PropertyChanged -= OnProfileDataChanged;
            geoLocationRepository.LocationServiceActivityChanged -= SetRadarActivityState;
        }

        private void BindToSynchronizerUpdatingEvent()
        {
            PropertyChanged += InvokeSaveDataToServer;
        }

        private void UnbindFromSynchronizerUpdatingEvent()
        {
            PropertyChanged -= InvokeSaveDataToServer;
        }

        private async void InvokeSaveDataToServer(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (!IsAllowedToSaveToServer) return;
            try
            {
                await SaveDataToServer().ConfigureAwait(true);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        private Task LoadAvatarImageFromServerAsync()
        {
            if (string.IsNullOrEmpty(UserProfileDataRemote.AvatarImageUrl))
            {
                LogUtility.PrintLog(Tag, "There is not URL to load user profile avatar image from", this);
                return Task.CompletedTask;
            }

            return downloadedSpritesRepository.CreateLoadTexture2DTask(UserProfileDataRemote.AvatarImageUrl,
                _cancellationController.TasksCancellationTokenSource.Token).ContinueWith(
                delegate(Task<Texture2D> iconLoadingTask)
                {
                    AvatarImage = iconLoadingTask.GetAwaiter().GetResult();

                    LogUtility.PrintLog(Tag, AvatarImage ? "User avatar image was loaded" : "User avatar image is null after being loaded",
                        this);
                }
                , TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        private void OnProfileDataChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == nameof(UserProfileDataRemote.AvatarImageUrl))
            {
                RefreshUserAvatar();
            }
        }

        private void RefreshUserAvatar()
        {
            if (!string.IsNullOrEmpty(UserProfileDataRemote.AvatarImageUrl)) return;
            AvatarImage = defaultAvatarImage;
        }

        public override Task LoadDataFromServer()
        {
            _isLoadingData = true;
            _cancellationController.CancelOngoingTask();
            return UserProfileDataSynchronization.LoadDataFromServer().ContinueWith(
                delegate
                {
                    return LoadAvatarImageFromServerAsync().ContinueWith(delegate { ConfirmDataLoading(); },
                        TaskContinuationOptions.OnlyOnRanToCompletion);
                }
                , TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        public override Task SaveDataToServer()
        {
            return UserProfileDataSynchronization.SaveDataToServer().ContinueWith(
                delegate { ConfirmDataSaved(); }, TaskContinuationOptions.OnlyOnRanToCompletion);
        }

        protected override void ConfirmDataLoading()
        {
            base.ConfirmDataLoading();
            _isLoadingData = false;
            LogUtility.PrintLog(Tag, "User profile data was loaded from server", this);
            LogUtility.PrintLog(Tag, JsonConvert.SerializeObject(UserProfileDataRemote), this);
        }

        protected override void ConfirmDataSaved()
        {
            base.ConfirmDataSaved();
            LogUtility.PrintLog(Tag, "User profile data was saved to server", this);
        }

        void IClearable.Clear()
        {
            UnbindFromSynchronizerUpdatingEvent();
            userProfileDataSynchronizer.Clear();
            BindToSynchronizerUpdatingEvent();
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => userProfileDataSynchronizer.PropertyChanged += value;
            remove => userProfileDataSynchronizer.PropertyChanged -= value;
        }
    }
}