using DataModels.Interfaces;

namespace DataModels
{
    public sealed class GameDataModel : IGameData
    {
        public string ChallengeType { get; set; }
        public string StartedAt { get; set; }
        public int Id { get; set; }
        public string Status { get; set; }
        public string Users { get; set; }
    }
}