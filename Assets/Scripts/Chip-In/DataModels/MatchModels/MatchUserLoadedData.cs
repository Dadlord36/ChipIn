using Newtonsoft.Json;
using UnityEngine;

namespace DataModels.MatchModels
{
    public struct MatchUserLoadedData
    {
        [JsonProperty("avatar")] public string AvatarUrl;
        [JsonProperty("score")] public uint Score;
        [JsonProperty("user_id")] public int UserId;
    }

    public struct MatchUserData
    {
        public Sprite AvatarSprite;
        public uint Score;
        public int UserId;

        public MatchUserData(in MatchUserLoadedData loadedData)
        {
            AvatarSprite = null;
            Score = loadedData.Score;
            UserId = loadedData.UserId;
        }
    }
}