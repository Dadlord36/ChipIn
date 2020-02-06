using DataModels.Interfaces;
using DataModels.MatchModels;

namespace DataModels
{
    public struct BaseMatchModel : IBaseMatchModel
    {
        public MatchUserData[] Users { get; set; }
        public int? WinnerId { get; set; }
        public float RoundsEndsAt { get; set; }
    }
}