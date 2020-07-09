using Newtonsoft.Json;

namespace DataModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class UserInterestLabelData
    {
        [JsonProperty("id")] public int Id;
        [JsonProperty("name")] public string Name;
        [JsonProperty("poster")] public string PosterUri;

        public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(PosterUri);
    }
}