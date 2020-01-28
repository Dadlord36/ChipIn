using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IScore
    {
        [JsonProperty("score")] uint Score { get; set; }
    }
}