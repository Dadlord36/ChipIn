using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class UpdateUserScoreResponseModel : IUpdateUserScoreResponseModel
    {
        public bool Success { get; set; }
        public uint Score { get; set; }
    }
}