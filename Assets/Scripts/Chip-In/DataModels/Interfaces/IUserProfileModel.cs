using DataModels.RequestsModels;
using GlobalVariables;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IBirthday
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Birthdate)] string Birthday { get; set; }
    }

    public interface ITokensBalance
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.TokensBalance)] int TokensBalance { get; set; }
    }

    public interface IShowAdsState
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.ShowAds)] bool ShowAdsState { get; set; }
    }

    public interface IShowAlertsState
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.ShowAlerts)] bool ShowAlertsState { get; set; }
    }

    public interface IUserRadarState
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.UserRadar)] bool UserRadarState { get; set; }
    }

    public interface IShowNotifications
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.ShowNotifications)] bool ShowNotificationsState { get; set; }
    }

    public interface ICurrencyCode
    {
        [JsonProperty(MainNames.ModelsPropertiesNames.Currency)] string CurrencyCode { get; set; }
    }

    public interface IUserAvatarUrl
    {
        [JsonProperty("avatar")]
        string Avatar { get; set; }
    }

    public interface IUserProfileModel : INamed, IEmail, IIdentifier, IRole, IGender, IBirthday, ICountryCode,ICurrencyCode, ITokensBalance, IShowAdsState,
        IShowAlertsState, IShowNotifications, IUserGeoLocation, IUserAvatarUrl,IUserRadarState
    {
       
    }
}