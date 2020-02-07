using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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

    public class MatchStateData
    {
        [JsonProperty("identifier")] public string Identifier { get; set; }
        [JsonProperty("message")] public MatchState MatchState { get; set; }
    }

    public sealed class GameChannelWebSocketSharp : WebSocket
    {
        #region Events Declaration

        public event Action<MatchStateData> RoundEnds, MatchEnds;

        #endregion

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
            const string accessTokenFieldName = "access-token";
            const string uIdFieldName = "uid";
            const string clientFieldName = "client";

            var dictionary = authenticationHeaders.ToDictionary(x => x.Key, x => x.Value,
                StringComparer.Ordinal);

            var stringBuilder = new StringBuilder($"{CableText}?");

            stringBuilder.Append(FormElement(accessTokenFieldName));
            AddNextElement(FormElement(uIdFieldName));
            AddNextElement(FormElement(clientFieldName));

            string FormElement(string key)
            {
                return key == accessTokenFieldName ? $"access_token={dictionary[key]}" : $"{key}={dictionary[key]}";
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

        private void OnSocketMessageReceived(object sender, MessageEventArgs messageEventArgs)
        {
            var data = messageEventArgs.Data;

               if(TryProcessStringAsMatchStateData(data)) return;
               if(TryProcessStringAsRegularMessage(data)) return;

               LogUtility.PrintLog(Tag, $"Data was not identified: {data}");
        }

        private bool TryProcessStringAsMatchStateData(string data)
        {
            if (!JsonConverterUtility.TryParseJson<MatchStateData>(data,out var matchStateData))
                return false;
                
            LogUtility.PrintLog(Tag, $"Match data: {data}");
            if (matchStateData.MatchState.Title == SlotsGameStatesNames.RoundEnd)
            {
                OnRoundEnds(matchStateData);
                return true;
            }
            OnMatchEnds(matchStateData);
            return true;
            
  
        }

        private static bool TryProcessStringAsRegularMessage(string data)
        {
            if (! JsonConverterUtility.TryParseJson<SocketMessage>(data, out var socketMessage))
                return false;

            if (socketMessage.Type == "ping")
            {
                LogUtility.PrintLog(Tag, "ping");
                return true;
            }

            LogUtility.PrintLog(Tag, $"Message is: {data}");
            return true;
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
            SubscribeToGameChannelWithJson();
        }

        private void SubscribeToGameChannelWithJson()
        {
            var commandString = JsonConvert.SerializeObject(ConnectionCommand);
            LogUtility.PrintLog(Tag, $"Channel subscribe command: {commandString}");
            Send(commandString);
        }

        #region Events Invokations

        private void OnRoundEnds(MatchStateData matchStateData)
        {
            RoundEnds?.Invoke(matchStateData);
        }

        private void OnMatchEnds(MatchStateData matchStateData)
        {
            MatchEnds?.Invoke(matchStateData);
        }

        #endregion
    }
}