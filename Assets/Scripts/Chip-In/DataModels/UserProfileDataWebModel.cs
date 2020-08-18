using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Structures;
using DataModels.Interfaces;
using GlobalVariables;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace DataModels
{
    public interface IUserMainData : INamed
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Role)] string Role { get; set; }
        [JsonProperty(MainNames.ModelsPropertiesNames.Email)] string Email { get; set; }
        [JsonProperty(MainNames.ModelsPropertiesNames.Gender)] string Gender { get; set; }
        [JsonProperty(MainNames.ModelsPropertiesNames.TokensBalance)] int TokensBalance { get; set; }
    }

    public interface IUserGeoLocation
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Location)] GeoLocation UserLocation { get; set; }
    }

    public interface IUserExtraData : IUserGeoLocation
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Birthdate)] string Birthday { get; set; }
    }

    public interface IUserPreferences
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.ShowAds)] bool ShowAdsState { get; set; }
        [JsonProperty(MainNames.ModelsPropertiesNames.ShowAlerts)] bool ShowAlertsState { get; set; }
        [JsonProperty(MainNames.ModelsPropertiesNames.UserRadar)] bool UserRadarState { get; set; }
        [JsonProperty(MainNames.ModelsPropertiesNames.ShowNotifications)] bool ShowNotificationsState { get; set; }
    }

    public interface ICountryCode
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Country)] string CountryCode { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public interface IUserProfileDataWebModel : IIdentifier, IUserMainData, IUserAvatarUrl, IUserExtraData,
        IUserPreferences, ICountryCode, ICurrencyCode
    {
    }

    public interface IDataLifeCycleModel : ICreatedAtTime
    {
        [JsonProperty("updated_at")] DateTime UpdatedAt { get; set; }
    }


    [Serializable]
    public class UserProfileDataWebModel : IUserProfileDataWebModel, INotifyPropertyChanged
    {
        [SerializeField] private int? id;
        [SerializeField] private string email;
        [SerializeField] private string name;
        [SerializeField] private string role;
        [SerializeField] private int tokensBalance;
        [SerializeField] private string gender;
        [SerializeField] private bool showAdsState;
        [SerializeField] private bool showAlertsState;
        [SerializeField] private bool userRadarState;
        [SerializeField] private bool showNotificationsState;
        [SerializeField] private GeoLocation userLocation;
        [SerializeField] private string avatarImageUrl;
        [SerializeField] private string birthday;
        [SerializeField] private string countryCode;
        private string _avatar;
        private string _currency;

        public UserProfileDataWebModel(int id, string email, string name, string role, int tokensBalance,
            string gender, bool showAdsState, bool showAlertsState, bool userRadarState, bool showNotificationsState,
            GeoLocation userLocation, string avatarImageUrl, string birthday, string countryCode)
        {
            this.id = id;
            this.email = email;
            this.name = name;
            this.role = role;
            this.tokensBalance = tokensBalance;
            this.gender = gender;
            this.showAdsState = showAdsState;
            this.showAlertsState = showAlertsState;
            this.userRadarState = userRadarState;
            this.showNotificationsState = showNotificationsState;
            this.userLocation = userLocation;
            this.avatarImageUrl = avatarImageUrl;
            this.birthday = birthday;
            this.countryCode = countryCode;
        }

        public static UserProfileDataWebModel Empty => new UserProfileDataWebModel(0, "", "", "", 0, "", false,
            false, false, false, new GeoLocation(), "", "", "");

        public int? Id
        {
            get => id;
            set
            {
                if (value == id) return;
                id = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => email;
            set
            {
                if (value == email) return;
                email = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        public string Role
        {
            get => role;
            set
            {
                if (value == role) return;
                role = value;
                OnPropertyChanged();
            }
        }

        public int TokensBalance
        {
            get => tokensBalance;
            set
            {
                if (value == tokensBalance) return;
                tokensBalance = value;
                OnPropertyChanged();
            }
        }

        public string Gender
        {
            get => gender;
            set
            {
                if (value == gender) return;
                gender = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAdsState
        {
            get => showAdsState;
            set
            {
                if (value == showAdsState) return;
                showAdsState = value;
                OnPropertyChanged();
            }
        }

        public bool ShowAlertsState
        {
            get => showAlertsState;
            set
            {
                if (value == showAlertsState) return;
                showAlertsState = value;
                OnPropertyChanged();
            }
        }

        public bool UserRadarState
        {
            get => userRadarState;
            set
            {
                if (value == userRadarState) return;
                userRadarState = value;
                OnPropertyChanged();
            }
        }

        public bool ShowNotificationsState
        {
            get => showNotificationsState;
            set
            {
                if (value == showNotificationsState) return;
                showNotificationsState = value;
                OnPropertyChanged();
            }
        }

        public GeoLocation UserLocation
        {
            get => userLocation;
            set
            {
                if (userLocation == value) return;
                userLocation = value;
                OnPropertyChanged();
            }
        }

        public string AvatarImageUrl
        {
            get => avatarImageUrl;
            set
            {
                if (value == avatarImageUrl) return;
                avatarImageUrl = value;
                OnPropertyChanged();
            }
        }

        public string Birthday
        {
            get => birthday;
            set
            {
                if (value == birthday) return;
                birthday = value;
                OnPropertyChanged();
            }
        }

        public string Avatar
        {
            get => _avatar;
            set
            {
                if (value == _avatar) return;
                _avatar = value;
                OnPropertyChanged();
            }
        }

        public string CountryCode
        {
            get => countryCode;
            set
            {
                if (value == countryCode) return;
                countryCode = value;
                OnPropertyChanged();
            }
        }

        public string CurrencyCode
        {
            get => _currency;
            set
            {
                if (value == _currency) return;
                _currency = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

       
    }
}