using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using GlobalVariables;
using Newtonsoft.Json;
using SuperSocket.ClientEngine;
using Utilities;
using WebSocket4Net;

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

    public struct SocketMessage
    {
        [JsonProperty("type")] public string Type { get; set; }

        [JsonProperty("message")] public int Message { get; set; }
    }

    public sealed class GameChannelSocket : WebSocket
    {
        private const string Tag = nameof(GameChannelSocket);
        private const string CableText = "cable";

        private static readonly SocketChannelSubscribeCommand ConnectionCommand =
            new SocketChannelSubscribeCommand(GameSocketsChannelsParameters.GameChannelName);

        public GameChannelSocket(IEnumerable<KeyValuePair<string, string>> authenticationHeaders) : base(
            uri:
            $"{GameSocketsChannelsParameters.ChipInHostUrl2}/{FormAuthenticationExtraString(authenticationHeaders)}",
            sslProtocols: SslProtocols.Ssl3 | SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls)
        {
            SubscribeOnSocketEvents();
        }

        private static string FormAuthenticationExtraString(
            IEnumerable<KeyValuePair<string, string>> authenticationHeaders)
        {
            var dictionary = authenticationHeaders.ToDictionary(x => x.Key, x => x.Value,
                StringComparer.Ordinal);

            var stringBuilder = new StringBuilder($"{CableText}?");

            stringBuilder.Append(FormElement("access-token"));
            AddNextElement(FormElement("uid"));
            AddNextElement(FormElement("client"));

            string FormElement(string key)
            {
                return key == "access-token" ? $"access_token={dictionary[key]}" : $"{key}={dictionary[key]}";
            }

            void AddNextElement(string element)
            {
                stringBuilder.Append($"&{element}");
            }

            return stringBuilder.ToString();
        }

        private void SubscribeOnSocketEvents()
        {
            Opened += OnSocketOpened;
            Closed += OnSocketClosed;
            Error += OnSocketError;
            MessageReceived += OnSocketMessageReceived;
            DataReceived += OnDataReceived;
        }

        private static void OnDataReceived(object sender, DataReceivedEventArgs e)
        {
            LogUtility.PrintLog(Tag, $"Data received: {e.Data}");
        }

        private static void OnSocketMessageReceived(object sender, MessageReceivedEventArgs messageReceivedEventArgs)
        {
            var message = JsonConvert.DeserializeObject<SocketMessage>(messageReceivedEventArgs.Message);
            LogUtility.PrintLog(Tag,
                message.Type == "ping" ? message.Type : $"Message received: {messageReceivedEventArgs.Message}");
        }

        private static void OnSocketError(object sender, ErrorEventArgs errorEventArgs)
        {
            LogUtility.PrintLogException(errorEventArgs.Exception);
        }

        private static void OnSocketClosed(object sender, EventArgs e)
        {
            LogUtility.PrintLog(Tag, $"Game channel socket is closed: {e}");
        }

        private void OnSocketOpened(object sender, EventArgs e)
        {
            LogUtility.PrintLog(Tag, $"Game channel socket is opened: {e}");
        }

        public void SubscribeToGameChannel()
        {
            SubscribeToGameChannelWithJson();
        }

        private void SubscribeToGameChannelWithJson()
        {
            var commandString = JsonConvert.SerializeObject(ConnectionCommand);
            LogUtility.PrintLog(Tag, $"Channel subscribe command: {commandString}");
            Send(commandString);
        }
    }
}