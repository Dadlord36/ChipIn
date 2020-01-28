using DataModels.MatchModels;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IShowMatchResponseModel : ISuccess
    {
        [JsonProperty("match")] MatchModel MatchData { get; set; }
        [JsonProperty("users")] MatchUserData[] Users { get; set; }
        [JsonProperty("background")] string BackgroundUrl { get; set; }
        [JsonProperty("stage")] int StageNumber { get; set; }
        [JsonProperty("winner_id")] int WinnerId { get; set; }
        [JsonProperty("round_ends_at")] float RoundsEndsAt { get; set; }
    }
}