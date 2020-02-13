using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Structures;
using DataModels.Interfaces;
using DataModels.RequestsModels;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace DataModels
{
    public interface IUserMainData
    {
        [JsonProperty("role")] string Role { get; set; }
        [JsonProperty("email")] string Email { get; set; }
        [JsonProperty("name")] string Name { get; set; }
        [JsonProperty("gender")] string Gender { get; set; }
        [JsonProperty("tokens_balance")] int TokensBalance { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public interface IUserAvatarSimpleModel
    {
        [JsonProperty("avatar")] string AvatarImageUrl { get; set; }
        Texture2D AvatarImage { get; set; }
    }

    public interface IUserExtraData
    {
        [JsonProperty("location")] GeoLocation UserLocation { get; set; }

        [JsonProperty("birthdate")] string Birthday { get; set; }
    }

    public interface IUserPreferences
    {
        [JsonProperty("show_ads")] bool ShowAdsState { get; set; }
        [JsonProperty("show_alerts")] bool ShowAlertsState { get; set; }
        [JsonProperty("user_radar")] bool UserRadarState { get; set; }
        [JsonProperty("show_notifications")] bool ShowNotificationsState { get; set; }
    }

    public interface ICountryCode
    {
        [JsonProperty("country")] string CountryCode { get; set; }
    }

    [JsonObject(MemberSerialization.OptIn)]
    public interface IUserProfileDataWebModel : IIdentifier, IUserMainData, IUserAvatarSimpleModel, IUserExtraData,
        IUserPreferences, ICountryCode
    {
        void Set(IUserProfileDataWebModel source);
    }

    public interface IDataLifeCycleModel
    {
        [JsonProperty("created_at")] DateTime CreatedAt { get; set; }

        [JsonProperty("updated_at")] DateTime UpdatedAt { get; set; }
    }


    [Serializable]
    public class UserProfileDataWebModel : IUserProfileDataWebModel, INotifyPropertyChanged
    {
        [SerializeField] private int id;
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
        [SerializeField] private Texture2D avatarImage;

        public UserProfileDataWebModel(int id, string email, string name, string role, int tokensBalance,
            string gender,
            bool showAdsState, bool showAlertsState, bool userRadarState, bool showNotificationsState,
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

        public int Id
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

        public Texture2D AvatarImage
        {
            get => avatarImage;
            set
            {
                if (Equals(value, avatarImage)) return;
                avatarImage = value;
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


        public void Set(IUserProfileDataWebModel source)
        {
            Id = source.Id;
            Email = source.Email;
            Name = source.Name;
            Role = source.Role;
            TokensBalance = source.TokensBalance;
            Gender = source.Gender;
            ShowAdsState = source.ShowAdsState;
            ShowAlertsState = source.ShowAlertsState;
            UserRadarState = source.UserRadarState;
            ShowNotificationsState = source.ShowNotificationsState;
            UserLocation = source.UserLocation;
            AvatarImageUrl = source.AvatarImageUrl;
            Birthday = source.Birthday;
            CountryCode = source.CountryCode;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}