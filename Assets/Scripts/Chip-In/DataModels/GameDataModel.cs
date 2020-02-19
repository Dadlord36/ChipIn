using System;
using DataModels.Interfaces;

namespace DataModels
{
    public sealed class GameDataModel : IGameModel
    {
        public string ChallengeType { get; set; }
        public DateTime StartedAt { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public string Users { get; set; }
    }
}