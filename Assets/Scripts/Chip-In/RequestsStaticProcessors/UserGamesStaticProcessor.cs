using System;
using System.Threading.Tasks;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.GetRequests;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class UserGamesStaticProcessor
    {
        public static async Task<GameDataModel[]> GetUserGames(IRequestHeaders requestHeaders)
        {
            try
            {
                var response = await new UserGamesGetProcessor(requestHeaders).SendRequest("User Games was retrieved");
                return response.ResponseModelInterface.Games;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<BaseRequestProcessor<object, JoinGameResponseDataModel, IJoinGameResponseModel>.HttpResponse>
            TryJoinAGame(IRequestHeaders requestHeaders, int gameId)
        {
            try
            {
                var response = await new JoinGamePostProcessor(requestHeaders, new[] {gameId.ToString(),
                        GameRequestParameters.Join}).SendRequest("User has successfully joined the game");
                return response;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<IShowMatchResponseModel> TryShowMatch(IRequestHeaders requestHeaders, int gameId)
        {
            try
            {
                var response = await new ShowMatchGetProcessor(requestHeaders, gameId).SendRequest(
                        "Matches data was retrieved successfully");
                return response.ResponseModelInterface;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<IUpdateUserScoreResponseModel> TryMakeAMove(IRequestHeaders requestHeaders, int gameId,
            SpinBoardParameters spinBoardParameters)
        {
            try
            {
                var response = await new MakeAMovePostProcessor(requestHeaders, gameId, spinBoardParameters).SendRequest(
                        "Player has made a move successfully");
                return response.ResponseModelInterface;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}