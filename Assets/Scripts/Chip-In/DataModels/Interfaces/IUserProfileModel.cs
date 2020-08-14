using DataModels.RequestsModels;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IBirthday
    {
        [JsonProperty("birthdate")] string Birthday { get; set; }
    }

    public interface ITokensBalance
    {
        [JsonProperty("tokens_balance")] int TokensBalance { get; set; }
    }

    public interface IShowAdsState
    {
        [JsonProperty("show_ads")] bool ShowAdsState { get; set; }
    }

    public interface IShowAlertsState
    {
        [JsonProperty("show_alerts")] bool ShowAlertsState { get; set; }
    }

    public interface IUserRadarState
    {
        [JsonProperty("user_radar")] bool UserRadarState { get; set; }
    }

    public interface IShowNotifications
    {
        [JsonProperty("show_notifications")] bool ShowNotificationsState { get; set; }
    }

    public interface IUserAvatarUrl
    {
        [JsonProperty("avatar")]
        string Avatar { get; set; }
    }

    public interface IUserProfileModel : INamed, IEmail, IIdentifier, IRole, IGender, IBirthday, ICountryCode, ITokensBalance, IShowAdsState,
        IShowAlertsState, IShowNotifications, IUserGeoLocation, IUserAvatarUrl,IUserRadarState
    {
       
    }
}