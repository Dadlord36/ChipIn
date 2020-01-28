using System;
using GlobalVariables;
using Newtonsoft.Json;
using WebSocket4Net;

namespace WebSockets
{
    public struct SocketCommand
    {
        public struct ChannelNameData
        {
            public ChannelNameData(string channelName)
            {
                ChannelName = channelName;
            }

            [JsonProperty("channel")] public string ChannelName;
        }

        public SocketCommand(string commandName, ChannelNameData[] identifier)
        {
            CommandName = commandName;
            Identifier = identifier;
        }

        [JsonProperty("command")] public string CommandName;
        [JsonProperty("identifier")] public ChannelNameData[] Identifier;
    }

    public class GameChannelSocket : WebSocket
    {
        public GameChannelSocket() : base(GameSocketsChannelsParameters.ChipInHostUrl)
        {
            Opened += OnSocketOpened;
        }

        private void OnSocketOpened(object sender, EventArgs e)
        {
            SubscribeToGameChannel();
            Opened -= OnSocketOpened;
        }

        private void SubscribeToGameChannel()
        {
            var command = new SocketCommand(SocketsCommands.Subscribe,
                identifier: new[] {new SocketCommand.ChannelNameData(GameSocketsChannelsParameters.GameChannelName)});
            
            Send(JsonConvert.SerializeObject(command));
        }
    }
}