using Newtonsoft.Json;

namespace DataModels.MatchModels
{
    public class MatchUserDownloadingData
    {
        [JsonProperty("avatar")] public string AvatarUrl;
        [JsonProperty("score")] public uint Score;
        [JsonProperty("user_id")] public int UserId;
    }
}