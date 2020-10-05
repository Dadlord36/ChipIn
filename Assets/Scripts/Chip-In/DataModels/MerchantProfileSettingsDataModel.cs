using Common.Structures;
using Repositories.Remote;

namespace DataModels
{
    public class MerchantProfileSettingsDataModel : IMerchantProfileSettingsModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int? Id { get; set; }
        public string Role { get; set; }
        public string Gender { get; set; }
        public string Birthday { get; set; }
        public string CountryCode { get; set; }
        public string CurrencyCode { get; set; }
        public int TokensBalance { get; set; }
        public bool ShowAdsState { get; set; }
        public bool ShowAlertsState { get; set; }
        public bool ShowNotificationsState { get; set; }
        public GeoLocation UserLocation { get; set; }
        public string Avatar { get; set; }
        public bool UserRadarState { get; set; }
        public string CompanyName { get; set; }
        public string CompanyEmail { get; set; }
        public string Slogan { get; set; }
        public bool SetReminderSAdCAdExpiring { get; set; }
        public string LogoUrl { get; set; }
    }
}