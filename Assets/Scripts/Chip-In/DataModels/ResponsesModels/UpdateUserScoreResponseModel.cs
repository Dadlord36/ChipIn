using DataModels.Interfaces;
using DataModels.MatchModels;

namespace DataModels.ResponsesModels
{
    public sealed class UpdateUserScoreResponseModel : IUpdateUserScoreResponseModel
    {
        public bool Success { get; set; }
        public uint Score { get; set; }
        public SlotsBoard Board { get; set; }
    }
}