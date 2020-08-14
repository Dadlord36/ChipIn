namespace DataModels.Extensions
{
    public static class DataModelsSetters
    {
        public static void Set(this IUserProfileDataWebModel instance, IUserProfileDataWebModel source)
        {
            instance.Avatar = source.Avatar;
            instance.Birthday = source.Birthday;
            instance.Email = source.Email;
            instance.Gender = source.Gender;
            instance.Id = source.Id;
            instance.Name = source.Name;
            instance.Role = source.Role;
            instance.CountryCode = source.CountryCode;
            instance.TokensBalance = source.TokensBalance;
            instance.UserLocation = source.UserLocation;
            instance.ShowAdsState = source.ShowAdsState;
            instance.ShowAlertsState = source.ShowAlertsState;
            instance.ShowNotificationsState = source.ShowNotificationsState;
            instance.UserRadarState = source.UserRadarState;
        }
    }
}