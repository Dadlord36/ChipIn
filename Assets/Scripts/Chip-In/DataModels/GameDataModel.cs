using System;
using DataModels.Interfaces;
using Newtonsoft.Json;

namespace DataModels
{
    public class Gameable : IIdentifier, IUrl
    {
        public int? Id { get; set; }
        [JsonProperty("gameable_type")] public string Type { get; set; }
        public string Url { get; set; }
    }

    public sealed class GameDataModel : IGameModel
    {
        public string ChallengeType { get; set; }
        public DateTime StartedAt { get; set; }

        [JsonProperty("gameable")] public Gameable GameableData { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public string Users { get; set; }
    }
}