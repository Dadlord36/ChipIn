using Newtonsoft.Json;

namespace DataModels.MatchModels
{
    public struct MatchBoardElementData
    {
        [JsonProperty("active")] public bool Activity;
        [JsonProperty("poster")] public string PosterUrl;
        [JsonProperty("icon_id")] public int IconId;
    }
}