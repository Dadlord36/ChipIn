using Newtonsoft.Json;

namespace WebSockets
{
    public struct SocketMessage
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("message")] public int Message { get; set; }
    }
}