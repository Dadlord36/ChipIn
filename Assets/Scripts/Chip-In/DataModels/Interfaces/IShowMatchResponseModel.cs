using DataModels.MatchModels;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IShowMatchResponseModel : ISuccess
    {
        [JsonProperty("match")] MatchDataModel MatchData { get; set; }
    }
}