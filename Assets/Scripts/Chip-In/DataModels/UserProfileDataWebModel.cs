﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Common.Structures;
using JetBrains.Annotations;
using Newtonsoft.Json;
using UnityEngine;

namespace DataModels
{
    public interface IUserProfileDataWebModel
    {
        [JsonProperty("id")] int Id { get; set; }
        [JsonProperty("email")] string Email { get; set; }
        [JsonProperty("name")] string Name { get; set; }
        [JsonProperty("role")] string Role { get; set; }
        [JsonProperty("tokens_balance")] int TokensBalance { get; set; }
        [JsonProperty("gender")] string Gender { get; set; }
        [JsonProperty("show_ads")] bool ShowAdsState { get; set; }
        [JsonProperty("show_alerts")] bool ShowAlertsState { get; set; }
        [JsonProperty("user_radar")] bool UserRadarState { get; set; }
        [JsonProperty("show_notifications")] bool ShowNotificationsState { get; set; }
        [JsonProperty("location")] GeoLocation UserLocation { get; set; }
        [JsonProperty("avatar")] string AvatarImageUrl { get; set; }
        [JsonProperty("birthdate")] string Birthday { get; set; }
        [JsonProperty("country")] string CountryCode { get; set; }
        void Set(IUserProfileDataWebModel source);
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
                if (Equals(value, userLocation)) return;
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