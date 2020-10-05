using System.ComponentModel;
using Common.Structures;
using DataModels.Interfaces;
using UnityEngine;

namespace Repositories.Remote
{
    [CreateAssetMenu(fileName = nameof(ScriptableMerchantProfileSettingsRepository),
        menuName = nameof(Repositories) + "/" + nameof(Remote) + "/" + nameof(ScriptableMerchantProfileSettingsRepository), order = 0)]
    public sealed class ScriptableMerchantProfileSettingsRepository : ScriptableRemoteRepositoryBase<MerchantProfileSettingsRepository>,
        IMerchantProfileSettings, INotifyPropertyChanged
    {
        public Sprite AvatarSprite
        {
            get => RemoteRepository.AvatarSprite;
            set => RemoteRepository.AvatarSprite = value;
        }

        public Sprite LogoSprite
        {
            get => RemoteRepository.LogoSprite;
            set => RemoteRepository.LogoSprite = value;
        }

        public string FirstName
        {
            get => RemoteRepository.FirstName;
            set => RemoteRepository.FirstName = value;
        }

        public string LastName
        {
            get => RemoteRepository.LastName;
            set => RemoteRepository.LastName = value;
        }

        public string Name
        {
            get => RemoteRepository.Name;
            set => RemoteRepository.Name = value;
        }

        public string Email
        {
            get => RemoteRepository.Email;
            set => RemoteRepository.Email = value;
        }

        public int? Id
        {
            get => RemoteRepository.Id;
            set => RemoteRepository.Id = value;
        }

        public string Role
        {
            get => RemoteRepository.Role;
            set => RemoteRepository.Role = value;
        }

        public string Gender
        {
            get => RemoteRepository.Gender;
            set => RemoteRepository.Gender = value;
        }

        public string Birthday
        {
            get => RemoteRepository.Birthday;
            set => RemoteRepository.Birthday = value;
        }

        public string CountryCode
        {
            get => RemoteRepository.CountryCode;
            set => RemoteRepository.CountryCode = value;
        }

        public string CurrencyCode
        {
            get => RemoteRepository.CurrencyCode;
            set => RemoteRepository.CurrencyCode = value;
        }

        public int TokensBalance
        {
            get => RemoteRepository.TokensBalance;
            set => RemoteRepository.TokensBalance = value;
        }

        public bool ShowAdsState
        {
            get => RemoteRepository.ShowAdsState;
            set => RemoteRepository.ShowAdsState = value;
        }

        public bool ShowAlertsState
        {
            get => RemoteRepository.ShowAlertsState;
            set => RemoteRepository.ShowAlertsState = value;
        }

        public bool ShowNotificationsState
        {
            get => RemoteRepository.ShowNotificationsState;
            set => RemoteRepository.ShowNotificationsState = value;
        }

        public GeoLocation UserLocation
        {
            get => RemoteRepository.UserLocation;
            set => RemoteRepository.UserLocation = value;
        }

        public string Avatar
        {
            get => RemoteRepository.Avatar;
            set => RemoteRepository.Avatar = value;
        }

        public bool UserRadarState
        {
            get => RemoteRepository.UserRadarState;
            set => RemoteRepository.UserRadarState = value;
        }

        public string CompanyName
        {
            get => RemoteRepository.CompanyName;
            set => RemoteRepository.CompanyName = value;
        }

        public string CompanyEmail
        {
            get => RemoteRepository.CompanyEmail;
            set => RemoteRepository.CompanyEmail = value;
        }

        public string Slogan
        {
            get => RemoteRepository.Slogan;
            set => RemoteRepository.Slogan = value;
        }

        public bool SetReminderSAdCAdExpiring
        {
            get => RemoteRepository.SetReminderSAdCAdExpiring;
            set => RemoteRepository.SetReminderSAdCAdExpiring = value;
        }

        public string LogoUrl
        {
            get => RemoteRepository.LogoUrl;
            set => RemoteRepository.LogoUrl = value;
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add => RemoteRepository.PropertyChanged += value;
            remove => RemoteRepository.PropertyChanged -= value;
        }
    }
}