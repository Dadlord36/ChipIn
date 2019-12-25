using Common.Structures;
using Newtonsoft.Json;

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

    public class UserProfileDataWebModel : IUserProfileDataWebModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        public int TokensBalance { get; set; }
        public string Gender { get; set; }
        public bool ShowAdsState { get; set; }
        public bool ShowAlertsState { get; set; }
        public bool UserRadarState { get; set; }
        public bool ShowNotificationsState { get; set; }
        public GeoLocation UserLocation { get; set; }
        public string AvatarImageUrl { get; set; }
        public string Birthday { get; set; }
        public string CountryCode { get; set; }

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
    }
}