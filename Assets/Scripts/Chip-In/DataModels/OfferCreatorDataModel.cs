﻿using System;
using Common.Structures;
using DataModels.Interfaces;
using Newtonsoft.Json;
using UnityEngine;

namespace DataModels
{
    public interface IUserAvatarModel
    {
        UserAvatar Avatar { get; set; }
    }

    public interface IOfferCreatorDataModel : IIdentifier, IUserMainData, IUserExtraData, IUserPreferences,
        IDataLifeCycleModel, IUserAvatarModel
    {
    }

    public class OfferCreatorDataModel : IOfferCreatorDataModel
    {
        public int Id { get; set; }
        public string Role { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int TokensBalance { get; set; }
        public GeoLocation UserLocation { get; set; }
        public UserAvatar Avatar { get; set; }
        public string Birthday { get; set; }
        public bool ShowAdsState { get; set; }
        public bool ShowAlertsState { get; set; }
        public bool UserRadarState { get; set; }
        public bool ShowNotificationsState { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}