using DataModels.Interfaces;
using DataModels.MatchModels;

namespace DataModels
{
    public struct BaseMatchModel : IBaseMatchModel
    {
        public SlotsBoardData BoardData { get; set; }
        public MatchUserDownloadingData[] Users { get; set; }
        public int? WinnerId { get; set; }
        public float RoundEndsAt { get; set; }
    }
}