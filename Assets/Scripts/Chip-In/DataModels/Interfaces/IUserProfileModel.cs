using System.ComponentModel;
using Common.Structures;
using UnityEngine;

namespace DataModels.Interfaces
{
    public interface IUserProfileModel
    {
        GeoLocation UserLocation { get; set; }
        Texture2D AvatarImage { get; set; }
        string Name { get; set; }
        int Id { get; set; }
        string Email { get; set; }
        string Role { get; set; }
        string Gender { get; set; }
        string Birthday { get; set; }
        string CountryCode { get; set; }
        int TokensBalance { get; set; }
        bool ShowAdsState { get; set; }
        bool ShowAlertsState { get; set; }
        bool UserRadarState { get; set; }
        bool ShowNotificationsState { get; set; }
    }
}