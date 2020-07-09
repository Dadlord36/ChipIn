using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IMemberMessage
    {
       [JsonProperty("member_message")] string MemberMessage { get; set; }
    }
}