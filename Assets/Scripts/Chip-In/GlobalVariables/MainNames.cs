namespace GlobalVariables
{
    public static class MainNames
    {
        public static class ModelsPropertiesNames
        {
            public const string Id = "id";
            public const string OwnerId = "owner_id";
            public const string Email = "email";
            public const string Password = "password";
            public const string PasswordConfirmation = "password_confirmation";
            public const string CurrentPassword = "current_password";
            public const string Gender = "gender";
            public const string Avatar = "avatar";
            public const string Name = "name";
            public const string Location = "location";
            public const string ShowNotifications = "show_notifications";
            public const string UserRadar = "user_radar";
            public const string ShowAlerts = "show_alerts";
            public const string Slogan = "slogan";
            public const string ShowAds = "show_ads";
            public const string Birthdate = "birthdate";
            public const string RadarRadius = "radar_radius";
            public const string Country = "country";
            public const string CountryCode = "country_code";
            public const string Role = "role";
            public const string TokensBalance = "tokens_balance";
            public const string Currency = "currency";
            public const string Category = "category";
            public const string Market = "market";
        }

        public enum InterestCategory
        {
            all,
            @join,
            my
        }

        public static class UserRoles
        {
            public const string BusinessOwner = "owner";
            public const string Client = "client";
            public const string Guest = "guest";
        }

        public static class OfferSegments
        {
            public const string Food = "food";
            public const string Travels = "travels";
            public const string Apparels = "apparels";
            public const string Gadgets = "gadgets";
            public const string Home = "home";
            public const string Transport = "transport";

            public static readonly string[] OffersSegmentsArray = {Food, Travels, Apparels, Gadgets, Home, Transport};
        }

        public static class OfferCategories
        {
            public const string TailoredOffer = "tailored";
            public const string BulkOffer = "bulk";

            public static readonly string[] OfferCategoriesArray = {TailoredOffer, BulkOffer};
        }

        public static class ChallengeTypes
        {
            public const string Match = "match";
            public const string Contest = "contes";
            public const string League = "league";
            public const string Tournament = "tournament";

            public static readonly string[] ChallengeTypesArray = {Match, Contest, League, Tournament};
        }

        public static class Pagination
        {
            public const string Page = "page";
            public const string PerPage = "per_page";
        }

        public static class CommonActions
        {
            public const string Support = "support";
            public const string Join = "join";
            public const string Fund = "fund";
            public const string Watch = "watch";
            public const string Leave = "leave";
        }
    }
}