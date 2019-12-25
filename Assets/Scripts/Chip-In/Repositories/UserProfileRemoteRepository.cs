using System;
using System.Threading.Tasks;
using Common.Structures;
using DataModels;
using DataModels.Interfaces;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Repositories.Synchronizers;
using ScriptableObjects.ActionsConnectors;
using UnityEngine;
using UnityEngine.UI;
using WebOperationUtilities;

namespace Repositories
{
    [CreateAssetMenu(fileName = nameof(UserProfileRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(UserProfileRemoteRepository), order = 0)]
    public sealed class UserProfileRemoteRepository : ScriptableObject, IUserProfileModel, IDataSynchronization
    {
        #region EventsDeclaration

        public event Action DataWasLoaded;
        public event Action DataWasSaved;

        #endregion

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        private IUserProfileDataWebModel _userProfileDataRemote;
        private IDataSynchronization _userProfileDataSynchronization;

        private Texture2D _userAvatarImage;

        #region IUserProfile delegation

        public GeoLocation UserLocation
        {
            get => _userProfileDataRemote.UserLocation;
            set => _userProfileDataRemote.UserLocation = value;
        }

        public Texture2D AvatarImage
        {
            get => _userAvatarImage;
            set => _userAvatarImage = value;
        }

        public string Name
        {
            get => _userProfileDataRemote.Name;
            set => _userProfileDataRemote.Name = value;
        }

        public int Id
        {
            get => _userProfileDataRemote.Id;
            set => _userProfileDataRemote.Id = value;
        }

        public string Email
        {
            get => _userProfileDataRemote.Email;
            set => _userProfileDataRemote.Email = value;
        }

        public string Role
        {
            get => _userProfileDataRemote.Role;
            set => _userProfileDataRemote.Role = value;
        }

        public string Gender
        {
            get => _userProfileDataRemote.Gender;
            set => _userProfileDataRemote.Gender = value;
        }

        public string Birthday
        {
            get => _userProfileDataRemote.Birthday;
            set => _userProfileDataRemote.Birthday = value;
        }

        public string CountryCode
        {
            get => _userProfileDataRemote.CountryCode;
            set => _userProfileDataRemote.CountryCode = value;
        }

        public int TokensBalance
        {
            get => _userProfileDataRemote.TokensBalance;
            set => _userProfileDataRemote.TokensBalance = value;
        }

        public bool ShowAdsState
        {
            get => _userProfileDataRemote.ShowAdsState;
            set => _userProfileDataRemote.ShowAdsState = value;
        }

        public bool ShowAlertsState
        {
            get => _userProfileDataRemote.ShowAlertsState;
            set => _userProfileDataRemote.ShowAlertsState = value;
        }

        public bool UserRadarState
        {
            get => _userProfileDataRemote.UserRadarState;
            set => _userProfileDataRemote.UserRadarState = value;
        }

        public bool ShowNotificationsState
        {
            get => _userProfileDataRemote.ShowNotificationsState;
            set => _userProfileDataRemote.ShowNotificationsState = value;
        }

        #endregion

        private void OnEnable()
        {
            var userProfileDataRemote = new UserProfileDataSynchronizer(authorisationDataRepository);

            _userProfileDataRemote = userProfileDataRemote;
            _userProfileDataSynchronization = userProfileDataRemote;
        }


        private async Task LoadAvatarImageFromServerAsync()
        {
            if (string.IsNullOrEmpty(_userProfileDataRemote.AvatarImageUrl))
            {
                Debug.Log("There is not URL to load user profile avatar image from",this);
                return;
            }
            _userAvatarImage = await ImagesDownloadingUtility.DownloadImageAsync(_userProfileDataRemote.AvatarImageUrl);
            if (_userAvatarImage)
                Debug.Log("User avatar image was loaded", this);
            else
            {
                Debug.Log("User avatar image is null after being loaded", this);
            }
        }

        public async Task LoadDataFromServer()
        {
            await _userProfileDataSynchronization.LoadDataFromServer();
            await LoadAvatarImageFromServerAsync();
            ConfirmDataLoading();
        }

        public async Task SaveDataToServer()
        {
            await _userProfileDataSynchronization.SaveDataToServer();
        }

        private void ConfirmDataLoading()
        {
            OnDataWasLoaded();
            Debug.Log("User profile data was loaded from server", this);
            Debug.Log(JsonConvert.SerializeObject(_userProfileDataRemote), this);
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
    }
}