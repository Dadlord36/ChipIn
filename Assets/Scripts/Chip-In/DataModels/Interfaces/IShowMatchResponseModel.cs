using DataModels.MatchModels;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IShowMatchResponseModel : ISuccess
    {
        [JsonProperty("match")] MatchModel MatchData { get; set; }
    }
}