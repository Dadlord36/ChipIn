using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Structures;
using DataModels.Interfaces;
using GlobalVariables;
using JetBrains.Annotations;
using Newtonsoft.Json;
using Tasking;

namespace DataModels
{
    public interface IUserMainData : INamed
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Role)]
        string Role { get; set; }

        [JsonProperty(MainNames.ModelsPropertiesNames.Email)]
        string Email { get; set; }

        [JsonProperty(MainNames.ModelsPropertiesNames.Gender)]
        string Gender { get; set; }

        [JsonProperty(MainNames.ModelsPropertiesNames.TokensBalance)]
        int TokensBalance { get; set; }
    }

    public interface IUserGeoLocation
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Location)]
        GeoLocation UserLocation { get; set; }
    }

    public interface IUserExtraData : IUserGeoLocation
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Birthdate)]
        string Birthday { get; set; }
    }

    public interface IUserPreferences
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.ShowAds)]
        bool ShowAdsState { get; set; }

        [JsonProperty(MainNames.ModelsPropertiesNames.ShowAlerts)]
        bool ShowAlertsState { get; set; }

        [JsonProperty(MainNames.ModelsPropertiesNames.UserRadar)]
        bool UserRadarState { get; set; }

        [JsonProperty(MainNames.ModelsPropertiesNames.ShowNotifications)]
        bool ShowNotificationsState { get; set; }
    }

    public interface ICountryCode
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Country)]
        string CountryCode { get; set; }
    }

    public interface IDataLifeCycleModel : ICreatedAtTime
    {
        [JsonProperty("updated_at")] DateTime UpdatedAt { get; set; }
    }

    public class UserProfileDataModel : IUserProfileModel, INotifyPropertyChanged
    {
        private int? id;
        private string email;
        private string name;
        private string role;
        private int tokensBalance;
        private string gender;
        private bool showAdsState;
        private bool showAlertsState;
        private bool userRadarState;
        private bool showNotificationsState;
        private GeoLocation userLocation;
        private string avatarImageUrl;
        private string birthday;
        private string countryCode;
        private string _avatar;
        private string _currency;

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
            TasksFactories.ExecuteOnMainThread(delegate { PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName)); });
        }
    }
}