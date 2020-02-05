using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using DataModels;
using GlobalVariables;
using Newtonsoft.Json;
using Utilities;
using WebSocketSharp;

namespace WebSockets
{
    public struct MatchState
    {
        [JsonProperty("body")] public BaseMatchModel Body { get; set; }
        [JsonProperty("round")] public int Round { get; set; }
        [JsonProperty("title")] public string Title { get; set; }
    }

    public class MathStateData
    {
        [JsonProperty("identifier")] public string Identifier { get; set; }
        [JsonProperty("message")] public MatchState MatchState { get; set; }
    }

    public sealed class GameChannelWebSocketSharp : WebSocket
    {
        private const string Tag = nameof(GameChannelSocket);
        private const string CableText = "cable";

        private static readonly SocketChannelSubscribeCommand ConnectionCommand =
            new SocketChannelSubscribeCommand(GameSocketsChannelsParameters.GameChannelName);


        public GameChannelWebSocketSharp(IEnumerable<KeyValuePair<string, string>> authenticationHeaders) : base(
            $"{GameSocketsChannelsParameters.ChipInHostUrl2}/{FormAuthenticationExtraString(authenticationHeaders)}")
        {
            Log.Level = LogLevel.Debug;

            SslConfiguration.EnabledSslProtocols =
                SslProtocols.Ssl3 | SslProtocols.Tls12 | SslProtocols.Tls11 | SslProtocols.Tls;
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
            OnOpen += OnSocketOpened;
            OnClose += OnSocketClosed;
            OnError += OnSocketError;
            OnMessage += OnSocketMessageReceived;
        }

        private static void OnSocketMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var data = messageEventArgs.Data;
            try
            {
                ProcessMathStateData(data);
            }
            catch
            {
                try
                {
                    ProcessSocketMessage(data);
                }
                catch
                {
                    LogUtility.PrintLog(Tag, messageEventArgs.Data);
                }
            }
        }

        private static void ProcessMathStateData(string data)
        {
            var matchStateData = JsonConvert.DeserializeObject<MathStateData>(data);
            LogUtility.PrintLog(Tag, JsonConvert.SerializeObject(matchStateData));
        }

        private static void ProcessSocketMessage(string data)
        {
            var socketMessage = JsonConvert.DeserializeObject<SocketMessage>(data);
            if (socketMessage.Type == "ping")
            {
                LogUtility.PrintLog(Tag, "ping");
                return;
            }

            LogUtility.PrintLog(Tag, data);
        }

        private static void OnSocketError(object sender, ErrorEventArgs eventArgs)
        {
            LogUtility.PrintLogException(eventArgs.Exception);
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