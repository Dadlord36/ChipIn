using DataModels.MatchModels;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IMatchModel
    {
        [JsonProperty("id")] int Id { get; set; }
        [JsonProperty("round")] uint RoundNumber { get; set; }
        [JsonProperty("board")] MatchBoardElementData[] BoardElements { get; set; }
    }
}