using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IText
    {
        [JsonProperty("text")] string Text { get; set; }
    }
}