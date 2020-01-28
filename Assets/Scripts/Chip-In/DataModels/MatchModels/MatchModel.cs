using DataModels.Interfaces;

namespace DataModels.MatchModels
{
    public sealed class MatchModel : IMatchModel
    {
        public int Id { get; set; }
        public uint RoundNumber { get; set; }
        public MatchBoardElementData[] BoardElements { get; set; }
    }
}