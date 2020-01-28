using DataModels.Interfaces;
using DataModels.MatchModels;
using Repositories.Interfaces;

namespace DataModels.ResponsesModels
{
    public sealed class UserGamesResponseModel : IUserGamesResponseModel
    {
        public bool Success { get; set; }

        public GameDataModel[] Games { get; set; }
    }
}