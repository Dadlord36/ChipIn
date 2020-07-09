using GlobalVariables;
using Newtonsoft.Json;

namespace WebSockets
{
    public struct SocketChannelSubscribeCommand
    {
        public SocketChannelSubscribeCommand(string channelName)
        {
            CommandName = SocketsCommands.Subscribe;
            Identifier = $"{{\"channel\":\"{channelName}\"}}";
        }

        [JsonProperty("command")] public string CommandName;
        [JsonProperty("identifier")] public string Identifier;
    }
}