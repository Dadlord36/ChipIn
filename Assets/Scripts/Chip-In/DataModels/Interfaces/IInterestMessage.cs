using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IInterestMessage
    {
        [JsonProperty("message")] string Message { get; set; }
    }
}