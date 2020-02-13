using DataModels.MatchModels;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IUserGamesResponseModel : ISuccess
    {
        [JsonProperty("games")] GameModelModel[] Games { get; set; }
    }
}