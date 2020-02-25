using DataModels.Interfaces;

namespace DataModels.MatchModels
{
    public sealed class MatchDataModel : IMatchModel
    {
        public int Id { get; set; }
        public uint RoundNumber { get; set; }
        public SlotsBoardData BoardData { get; set; }
        public MatchUserDownloadingData[] Users { get; set; }
        public string BackgroundUrl { get; set; }
        public int StageNumber { get; set; }
        public int? WinnerId { get; set; }
        public float RoundEndsAt { get; set; }
        public IndexedUrl[] IndexedSpritesSheetsUrls { get; set; }
    }
}