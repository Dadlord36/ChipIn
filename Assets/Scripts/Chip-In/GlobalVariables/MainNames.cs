﻿using System;

namespace GlobalVariables
{
    public static class MainNames
    {
        public static class ModelsPropertiesNames
        {
            public const string InterestId = "interest_id";
            public const string Icon = "icon";
            public const string Description = "description";
            public const string Poster = "poster";
            public const string Id = "id";
            public const string OwnerId = "owner_id";
            public const string Email = "email";
            public const string Password = "password";
            public const string PasswordConfirmation = "password_confirmation";
            public const string CurrentPassword = "current_password";
            public const string Gender = "gender";
            public const string Avatar = "avatar";
            public const string Logo = "logo";
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
            public const string TokensAmount = "tokens_amount";
            public const string Currency = "currency";
            public const string Category = "category";
            public const string Market = "market";
            public const string CompanyName = "company_name";
            public const string CompanyEmail = "company_email";
            public const string SetReminderSAdCAdExpiring = "set_reminder";
            public const string Offer = "offer";
        }

        public enum InterestCategory
        {
            all,
            @join,
            my
        }

        public static class OfferLifeTypes
        {
            public const string ShortTerm = "short";
            public const string LongTerm = "long";

            public static string GetOfferLifeTypeName(int value)
            {
                switch (value)
                {
                    case 0:
                    {
                        return ShortTerm;
                    }
                    case 1:
                    {
                        return LongTerm;
                    }
                }

                throw new ArgumentOutOfRangeException($"Number {value.ToString()} is out of range");
            }
        }

        public static class CurrencyNames
        {
            public const string Cash = "cash";
            public const string Tokens = "tokens";
            public const string Cash_Tokens = "cash/tokens";

            public static string GetCurrencyName(int number)
            {
                switch (number)
                {
                    case 0:
                    {
                        return Cash;
                    }
                    case 1:
                    {
                        return Tokens;
                    }
                    case 2:
                    {
                        return Cash_Tokens;
                    }
                }

                throw new ArgumentOutOfRangeException($"Number {number.ToString()} is out of range");
            }
        }

        public static class FlashOfferCategories
        {
            public const string FivePlayersChallenge = "players_challenge";
            public const string FreeItem = "free_item";

            public static string GetFlashOfferCategoryName(int number)
            {
                switch (number)
                {
                    case 0:
                    {
                        return FivePlayersChallenge;
                    }
                    case 1:
                    {
                        return FreeItem;
                    }
                }

                throw new ArgumentOutOfRangeException($"Number {number.ToString()} is out of range");
            }
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