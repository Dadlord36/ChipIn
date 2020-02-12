using DataModels.MatchModels;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ISlotsBoard
    {
        [JsonProperty("board")] SlotsBoard Board { get; set; }
    }

    public interface IGameWinnerIdentifier
    {
        [JsonProperty("winner_id")] int? WinnerId { get; set; }
    }

    public interface IGameUsers
    {
        [JsonProperty("users")] MatchUserLoadedData[] Users { get; set; }
    }

    public interface IBaseMatchModel : ISlotsBoard, IGameWinnerIdentifier, IGameUsers
    {
        [JsonProperty("round_ends_at")] float RoundEndsAt { get; set; }
    }

    public interface IMatchModel : IBaseMatchModel
    {
        [JsonProperty("id")] int Id { get; set; }
        [JsonProperty("round")] uint RoundNumber { get; set; }
        [JsonProperty("background")] string BackgroundUrl { get; set; }
        [JsonProperty("stage")] int StageNumber { get; set; }
    }
}