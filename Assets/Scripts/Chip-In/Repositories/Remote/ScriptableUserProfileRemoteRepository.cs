using System.ComponentModel;
using Common.Structures;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(ScriptableUserProfileRemoteRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(ScriptableUserProfileRemoteRepository), order = 0)]
    public sealed class ScriptableUserProfileRemoteRepository : ScriptableRemoteRepositoryBase<UserProfileRemoteRepository>, IUserProfileRemoteRepository
    {
        private IUserProfileRemoteRepository UserProfileRemoteRepositoryImplementation => RemoteRepository;

        private void OnEnable()
        {
            RemoteRepository.SubscribeOnEvents();
        }
        
        private void OnDisable()
        {
            RemoteRepository.UnsubscribeFromEvents();
        }

        public string Name
        {
            get => UserProfileRemoteRepositoryImplementation.Name;
            set => UserProfileRemoteRepositoryImplementation.Name = value;
        }

        public string Email
        {
            get => UserProfileRemoteRepositoryImplementation.Email;
            set => UserProfileRemoteRepositoryImplementation.Email = value;
        }

        public int? Id
        {
            get => UserProfileRemoteRepositoryImplementation.Id;
            set => UserProfileRemoteRepositoryImplementation.Id = value;
        }

        public string Role
        {
            get => UserProfileRemoteRepositoryImplementation.Role;
            set => UserProfileRemoteRepositoryImplementation.Role = value;
        }

        public string Gender
        {
            get => UserProfileRemoteRepositoryImplementation.Gender;
            set => UserProfileRemoteRepositoryImplementation.Gender = value;
        }

        public string Birthday
        {
            get => UserProfileRemoteRepositoryImplementation.Birthday;
            set => UserProfileRemoteRepositoryImplementation.Birthday = value;
        }

        public string CountryCode
        {
            get => UserProfileRemoteRepositoryImplementation.CountryCode;
            set => UserProfileRemoteRepositoryImplementation.CountryCode = value;
        }

        public string CurrencyCode
        {
            get => UserProfileRemoteRepositoryImplementation.CurrencyCode;
            set => UserProfileRemoteRepositoryImplementation.CurrencyCode = value;
        }

        public int TokensBalance
        {
            get => UserProfileRemoteRepositoryImplementation.TokensBalance;
            set => UserProfileRemoteRepositoryImplementation.TokensBalance = value;
        }

        public bool ShowAdsState
        {
            get => UserProfileRemoteRepositoryImplementation.ShowAdsState;
            set => UserProfileRemoteRepositoryImplementation.ShowAdsState = value;
        }

        public bool ShowAlertsState
        {
            get => UserProfileRemoteRepositoryImplementation.ShowAlertsState;
            set => UserProfileRemoteRepositoryImplementation.ShowAlertsState = value;
        }

        public bool ShowNotificationsState
        {
            get => UserProfileRemoteRepositoryImplementation.ShowNotificationsState;
            set => UserProfileRemoteRepositoryImplementation.ShowNotificationsState = value;
        }

        public GeoLocation UserLocation
        {
            get => UserProfileRemoteRepositoryImplementation.UserLocation;
            set => UserProfileRemoteRepositoryImplementation.UserLocation = value;
        }

        public string Avatar
        {
            get => UserProfileRemoteRepositoryImplementation.Avatar;
            set => UserProfileRemoteRepositoryImplementation.Avatar = value;
        }

        public bool UserRadarState
        {
            get => UserProfileRemoteRepositoryImplementation.UserRadarState;
            set => UserProfileRemoteRepositoryImplementation.UserRadarState = value;
        }

        public void Clear()
        {
            UserProfileRemoteRepositoryImplementation.Clear();
        }
        
        public Sprite UserAvatarSprite
        {
            get => UserProfileRemoteRepositoryImplementation.UserAvatarSprite;
            set => UserProfileRemoteRepositoryImplementation.UserAvatarSprite = value;
        }

        public void SubscribeOnEvents()
        {
            UserProfileRemoteRepositoryImplementation.SubscribeOnEvents();
        }

        public void UnsubscribeFromEvents()
        {
            UserProfileRemoteRepositoryImplementation.UnsubscribeFromEvents();
        }
        
        public event PropertyChangedEventHandler PropertyChanged
        {
            add => UserProfileRemoteRepositoryImplementation.PropertyChanged += value;
            remove => UserProfileRemoteRepositoryImplementation.PropertyChanged -= value;
        }
    }
}