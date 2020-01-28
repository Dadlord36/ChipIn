using DataModels.Interfaces;
using DataModels.MatchModels;

namespace DataModels.ResponsesModels
{
    public class ShowMatchResponseModel : IShowMatchResponseModel
    {
        public bool Success { get; set; }
        public MatchModel MatchData { get; set; }
        public MatchUserData[] Users { get; set; }
        public string BackgroundUrl { get; set; }
        public int StageNumber { get; set; }
        public int WinnerId { get; set; }
        public float RoundsEndsAt { get; set; }
    }
}