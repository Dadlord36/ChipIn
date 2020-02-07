using DataModels.MatchModels;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IBaseMatchModel
    {
        [JsonProperty("board")] SlotsBoard Board { get; set; }
        [JsonProperty("users")] MatchUserData[] Users { get; set; }
        [JsonProperty("winner_id")] int? WinnerId { get; set; }
        [JsonProperty("round_ends_at")] float RoundsEndsAt { get; set; }
    }

    public interface IMatchModel : IBaseMatchModel
    {
        [JsonProperty("id")] int Id { get; set; }
        [JsonProperty("round")] uint RoundNumber { get; set; }
        [JsonProperty("background")] string BackgroundUrl { get; set; }
        [JsonProperty("stage")] int StageNumber { get; set; }
    }
}