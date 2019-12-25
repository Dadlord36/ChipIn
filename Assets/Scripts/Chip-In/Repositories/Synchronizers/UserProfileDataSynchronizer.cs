using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Common.Structures;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using JetBrains.Annotations;
using Repositories.Interfaces;
using RequestsStaticProcessors;

namespace Repositories.Synchronizers
{
    public sealed class UserProfileDataSynchronizer : IUserProfileDataWebModel, IDataSynchronization,
        INotifyPropertyChanged
    {
        #region EventsDeclaretion

        public event Action DataWasLoaded;
        public event Action DataWasSaved;

        #endregion


        private readonly IUserProfileDataWebModel _userProfile = new UserProfileDataWebModel();
        private readonly IRequestHeaders _requestHeaders;

        public UserProfileDataSynchronizer(IRequestHeaders headers)
        {
            _requestHeaders = headers;
        }

        public void Set(IUserProfileDataWebModel source)
        {
            _userProfile.Set(source);
        }

        public int Id
        {
            get => _userProfile.Id;
            set => _userProfile.Id = value;
        }

        public string Email
        {
            get => _userProfile.Email;
            set => _userProfile.Email = value;
        }

        public string Name
        {
            get => _userProfile.Name;
            set => _userProfile.Name = value;
        }

        public string Role
        {
            get => _userProfile.Role;
            set => _userProfile.Role = value;
        }

        public int TokensBalance
        {
            get => _userProfile.TokensBalance;
            set => _userProfile.TokensBalance = value;
        }

        public string Gender
        {
            get => _userProfile.Gender;
            set => _userProfile.Gender = value;
        }

        public bool ShowAdsState
        {
            get => _userProfile.ShowAdsState;
            set => _userProfile.ShowAdsState = value;
        }

        public bool ShowAlertsState
        {
            get => _userProfile.ShowAlertsState;
            set => _userProfile.ShowAlertsState = value;
        }

        public bool UserRadarState
        {
            get => _userProfile.UserRadarState;
            set => _userProfile.UserRadarState = value;
        }

        public bool ShowNotificationsState
        {
            get => _userProfile.ShowNotificationsState;
            set => _userProfile.ShowNotificationsState = value;
        }

        public GeoLocation UserLocation
        {
            get => _userProfile.UserLocation;
            set => _userProfile.UserLocation = value;
        }

        public string AvatarImageUrl
        {
            get => _userProfile.AvatarImageUrl;
            set => _userProfile.AvatarImageUrl = value;
        }

        public string Birthday
        {
            get => _userProfile.Birthday;
            set => _userProfile.Birthday = value;
        }

        public string CountryCode
        {
            get => _userProfile.CountryCode;
            set => _userProfile.CountryCode = value;
        }

        public async Task LoadDataFromServer()
        {
            var response = await UserProfileDataStaticRequestsProcessor.GetUserProfileData(_requestHeaders);
            _userProfile.Set(response.ResponseModelInterface);
            ConfirmDataLoading();
        }

        public async Task SaveDataToServer()
        {    
            await UserProfileDataStaticRequestsProcessor.UpdateUserProfileData(_requestHeaders, _userProfile);
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}