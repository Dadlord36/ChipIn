using DataModels.Interfaces;

namespace DataModels.MatchModels
{
    public sealed class MatchModel : IMatchModel
    {
        public int Id { get; set; }
        public uint RoundNumber { get; set; }
        public SlotsBoard Board { get; set; }
        public MatchUserLoadedData[] Users { get; set; }
        public string BackgroundUrl { get; set; }
        public int StageNumber { get; set; }
        public int? WinnerId { get; set; }
        public float RoundsEndsAt { get; set; }
    }
}