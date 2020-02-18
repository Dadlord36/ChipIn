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
using WebOperationUtilities;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(UserProfileRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(UserProfileRemoteRepository), order = 0)]
    public sealed class UserProfileRemoteRepository : RemoteRepositoryBase, IUserProfileModel, IClearable,
        INotifyPropertyChanged
    {
        [SerializeField] private UserProfileDataSynchronizer userProfileDataSynchronizer;
        [SerializeField] private SessionStateRepository sessionStateRepository;

        private IUserProfileDataWebModel UserProfileDataRemote => userProfileDataSynchronizer;
        private IDataSynchronization UserProfileDataSynchronization => userProfileDataSynchronizer;
        private ILoginState SessionState => sessionStateRepository;

        private bool IsAllowedToSaveToServer => !_isLoadingData && SessionState.IsLoggedIn;

        [SerializeField] private Texture2D defaultAvatarImage;

        private bool _isLoadingData;

        #region IUserProfile delegation

        private int _coinsGameResult; 
        
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
            BindToSynchronizerUpdatingEvent();
            PropertyChanged += OnProfileDataChanged;
        }


        private void OnDisable()
        {
            UnbindFromSynchronizerUpdatingEvent();
            PropertyChanged -= OnProfileDataChanged;
        }

        private void BindToSynchronizerUpdatingEvent()
        {
            PropertyChanged += InvokeSaveDataToServer;
        }

        private void UnbindFromSynchronizerUpdatingEvent()
        {
            PropertyChanged -= InvokeSaveDataToServer;
        }

        private void InvokeSaveDataToServer(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (IsAllowedToSaveToServer)
                SaveDataToServer();
        }

        private async Task LoadAvatarImageFromServerAsync()
        {
            if (string.IsNullOrEmpty(UserProfileDataRemote.AvatarImageUrl))
            {
                Debug.Log("There is not URL to load user profile avatar image from", this);
                return;
            }

            AvatarImage = await ImagesDownloadingUtility.DownloadImageAsync(UserProfileDataRemote.AvatarImageUrl);
            Debug.Log(AvatarImage ? "User avatar image was loaded" : "User avatar image is null after being loaded",
                this);
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

        public override async Task LoadDataFromServer()
        {
            _isLoadingData = true;
            await UserProfileDataSynchronization.LoadDataFromServer();
            await LoadAvatarImageFromServerAsync();
            ConfirmDataLoading();
        }

        public override async Task SaveDataToServer()
        {
            await UserProfileDataSynchronization.SaveDataToServer();
            ConfirmDataSaved();
        }

        protected override void ConfirmDataLoading()
        {
            base.ConfirmDataLoading();
            _isLoadingData = false;
            Debug.Log("User profile data was loaded from server", this);
            Debug.Log(JsonConvert.SerializeObject(UserProfileDataRemote), this);
        }

        protected override void ConfirmDataSaved()
        {
            base.ConfirmDataSaved();
            Debug.Log("User profile data was saved to server", this);
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