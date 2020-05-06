using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ISlogan
    {
        [JsonProperty("slogan")] string Slogan { get; set; }
    }
}