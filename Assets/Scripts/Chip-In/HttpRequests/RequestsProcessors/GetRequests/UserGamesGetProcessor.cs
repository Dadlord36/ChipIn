using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.MatchModels;
using DataModels.ResponsesModels;
using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public struct SpinBoardParameters
    {
        public readonly bool SpinFrame, SpinBoard;

        private SpinBoardParameters(bool spinFrame, bool spinBoard)
        {
            SpinFrame = spinFrame;
            SpinBoard = spinBoard;
        }

        public static SpinBoardParameters JustFrame = new SpinBoardParameters(true, false);
        public static SpinBoardParameters JustBoard = new SpinBoardParameters(false, true);
        public static SpinBoardParameters Both = new SpinBoardParameters(true, true);
    }

    public class UserGamesGetProcessor : RequestWithoutBodyProcessor<UserGamesResponseModel, IUserGamesResponseModel>
    {
        public UserGamesGetProcessor(IRequestHeaders requestHeaders) : base(ApiCategories.UserGames, HttpMethod.Get,
            requestHeaders, null)
        {
        }
    }

    public interface IIconsBackgroundUrl
    {
        [JsonProperty("background")] string BackgroundUrl { get; set; }
    }

    public interface ISlotGameIconsSet
    {
        [JsonProperty("icons")] IndexedUrl[] Icons { get; set; }
    }

    public interface IJoinGameResponseModel : ISuccess
    {
        [JsonProperty("game")] GameBoardData GameBoard { get; set; }
    }

    public class GameBoardData : IIconsBackgroundUrl, ISlotGameIconsSet
    {
        public string BackgroundUrl { get; set; }
        public IndexedUrl[] Icons { get; set; }
    }

    public sealed class JoinGameResponseDataModel : IJoinGameResponseModel
    {
        public bool Success { get; set; }
        public GameBoardData GameBoard { get; set; }
    }

    public class JoinGamePostProcessor : RequestWithoutBodyProcessor<JoinGameResponseDataModel, IJoinGameResponseModel>
    {
        public JoinGamePostProcessor(IRequestHeaders requestHeaders, IReadOnlyList<string> requestParameters) :
            base(ApiCategories.Games, HttpMethod.Post, requestHeaders, requestParameters)
        {
        }
    }

    public class
        MakeAMovePostProcessor : RequestWithoutBodyProcessor<UpdateUserScoreResponseModel, IUpdateUserScoreResponseModel
        >
    {
        public MakeAMovePostProcessor(IRequestHeaders requestHeaders, int gameId, SpinBoardParameters spinBoardParameters)
            : base(ApiCategories.Games, HttpMethod.Post, requestHeaders, new[]
            {
                gameId.ToString(), GameRequestParameters.Match, GameRequestParameters.Move
            }, FormNameValueCollectionForQueryStringParameters(spinBoardParameters))
        {
        }

        private static NameValueCollection FormNameValueCollectionForQueryStringParameters(
            SpinBoardParameters spinBoardParameters)
        {
            var collection = new NameValueCollection(2) 
                {{GameRequestParameters.SpinFrame, GameRequestParameters.ConvertBoolToStringText(spinBoardParameters.SpinFrame)}, 
                    {GameRequestParameters.SpinIcons, GameRequestParameters.ConvertBoolToStringText(spinBoardParameters.SpinBoard)}};
            return collection;
        }
    }
}