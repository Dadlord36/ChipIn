using System.Threading.Tasks;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class UserGamesStaticProcessor
    {
        /*public static Task<BaseRequestProcessor<object, UserGamesResponseModel, IUserGamesResponseModel>.HttpResponse> GetUserGames(
            out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders)
        {
            return new UserGamesGetProcessor(out cancellationTokenSource, requestHeaders).SendRequest("User Games was retrieved");
        }*/

        public static Task<BaseRequestProcessor<object, JoinGameResponseDataModel, IJoinGameResponseModel>.HttpResponse>
            TryJoinAGame(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int gameId)
        {
            return new JoinGamePostProcessor(out cancellationTokenSource, requestHeaders, new[]
            {
                gameId.ToString(),
                GameRequestParameters.Join
            }).SendRequest("User has successfully joined the game");
        }

        public static Task<BaseRequestProcessor<object, ShowMatchResponseModel, IShowMatchResponseModel>.HttpResponse>
            TryShowMatch(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int gameId)
        {
            return new ShowMatchGetProcessor(out cancellationTokenSource, requestHeaders, gameId).SendRequest(
                "Matches data was retrieved successfully");
        }

        public static Task<BaseRequestProcessor<object, UpdateUserScoreResponseModel, IUpdateUserScoreResponseModel>.HttpResponse>
            TryMakeAMove(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int gameId,
                SpinBoardParameters spinBoardParameters)
        {
            return new MakeAMovePostProcessor(out cancellationTokenSource, requestHeaders, gameId, spinBoardParameters).SendRequest(
                "Player has made a move successfully");
        }
    }
}