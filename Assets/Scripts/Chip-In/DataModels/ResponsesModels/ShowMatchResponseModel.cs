using DataModels.Interfaces;
using DataModels.MatchModels;

namespace DataModels.ResponsesModels
{
    public sealed class ShowMatchResponseModel : IShowMatchResponseModel
    {
        public bool Success { get; set; }
        public MatchModel MatchData { get; set; }
    }
}