using DataModels.Interfaces;
using Repositories.Remote;

namespace DataModels.Extensions
{
    public static class DataModelsSetters
    {
        public static void Set(this IUserProfileModel instance, IUserProfileModel source)
        {
            instance.Avatar = source.Avatar;
            instance.Birthday = source.Birthday;
            instance.Email = source.Email;
            instance.Gender = source.Gender;
            instance.Id = source.Id;
            instance.Name = source.Name;
            instance.Role = source.Role;
            instance.CountryCode = source.CountryCode;
            instance.CurrencyCode = source.CurrencyCode;
            instance.TokensBalance = source.TokensBalance;
            instance.UserLocation = source.UserLocation;
            instance.ShowAdsState = source.ShowAdsState;
            instance.ShowAlertsState = source.ShowAlertsState;
            instance.ShowNotificationsState = source.ShowNotificationsState;
            instance.UserRadarState = source.UserRadarState;
        }

        public static void Set(this IMerchantProfileSettingsModel instance, IMerchantProfileSettingsModel source)
        {
            instance.Set(source as IUserProfileModel);
            instance.SetReminderSAdCAdExpiring = source.SetReminderSAdCAdExpiring;
            instance.CompanyName = source.CompanyName;
            instance.CompanyEmail = source.CompanyEmail;
            instance.LogoUrl = source.LogoUrl;
            instance.Slogan = source.Slogan;
        }

        public static void Set(this IMerchantProfileSettings instance, IMerchantProfileSettings source)
        {
            instance.Set(source as IMerchantProfileSettingsModel);
            instance.AvatarSprite = source.AvatarSprite;
            instance.LogoSprite = source.LogoSprite;
            instance.FirstName = source.FirstName;
            instance.LastName = source.LastName;
        }

        public static void Set(this IFlashOfferGetRequestModel instance, IFlashOfferGetRequestModel source)
        {
            instance.Description = source.Description;
            instance.Period = source.Period;
            instance.Price = source.Price;
            instance.Quantity = source.Quantity;
            instance.Radius = source.Radius;
            instance.Title = source.Title;
            instance.ExpireDate = source.ExpireDate;
            instance.PriceType = source.PriceType;
        }

        public static void Set(this IAdvertFeatureBaseModel instance, IAdvertFeatureBaseModel source)
        {
            instance.Description = source.Description;
            instance.Icon = source.Icon;
            instance.TokensAmount = source.TokensAmount;
        }
    }
}