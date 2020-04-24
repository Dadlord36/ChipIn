namespace GlobalVariables
{
    public static class MainNames
    {
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

        public static class CommunityActions
        {
            public const string Leave = "leave";
            public const string Join = "join";
        }
    }
}