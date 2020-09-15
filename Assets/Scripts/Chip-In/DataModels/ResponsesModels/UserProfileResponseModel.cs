using Common.Structures;
using DataModels.Interfaces;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public interface IUserProfileResponseModel : IUserProfileModel, ISuccess
    {
    }

    public sealed class UserProfileResponseModel : IUserProfileResponseModel
    {
        public bool Success { get; set; }
        [JsonProperty("user")] public UserProfileDataModel User { get; set; }
        [JsonProperty("auth")] public AuthorisationModel Authorisation { get; set; }

        public int? Id
        {
            get => User.Id;
            set => User.Id = value;
        }

        public string Email
        {
            get => User.Email;
            set => User.Email = value;
        }

        public string Name
        {
            get => User.Name;
            set => User.Name = value;
        }

        public string Role
        {
            get => User.Role;
            set => User.Role = value;
        }

        public int TokensBalance
        {
            get => User.TokensBalance;
            set => User.TokensBalance = value;
        }

        public string Gender
        {
            get => User.Gender;
            set => User.Gender = value;
        }

        public bool ShowAdsState
        {
            get => User.ShowAdsState;
            set => User.ShowAdsState = value;
        }

        public bool ShowAlertsState
        {
            get => User.ShowAlertsState;
            set => User.ShowAlertsState = value;
        }

        public bool UserRadarState
        {
            get => User.UserRadarState;
            set => User.UserRadarState = value;
        }

        public bool ShowNotificationsState
        {
            get => User.ShowNotificationsState;
            set => User.ShowNotificationsState = value;
        }

        public GeoLocation UserLocation
        {
            get => User.UserLocation;
            set => User.UserLocation = value;
        }

        public string Birthday
        {
            get => User.Birthday;
            set => User.Birthday = value;
        }

        public string CountryCode
        {
            get => User.CountryCode;
            set => User.CountryCode = value;
        }

        public string Avatar
        {
            get => User.Avatar;
            set => User.Avatar = value;
        }

        public string CurrencyCode
        {
            get => User.CurrencyCode;
            set => User.CurrencyCode = value;
        }
    }
}