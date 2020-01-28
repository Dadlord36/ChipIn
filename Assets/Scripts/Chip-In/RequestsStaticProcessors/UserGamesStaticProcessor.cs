using System.Threading.Tasks;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using GlobalVariables;
using HttpRequests.RequestsProcessors.GetRequests;

namespace RequestsStaticProcessors
{
    public static class UserGamesStaticProcessor
    {
        public static async Task<GameDataModel[]> GetUserGames(IRequestHeaders requestHeaders)
        {
            var response = await new UserGamesGetProcessor(requestHeaders).SendRequest("User Games was retrieved");
            return response.ResponseModelInterface.Games;
        }

        public static async Task<bool> JoinAGame(IRequestHeaders requestHeaders, int gameId)
        {
            var response =
                await new JoinGamePostProcessor(requestHeaders, new[] {gameId.ToString(), GameRequestParameters.Join})
                    .SendRequest("User has successfully joined the game");
            return response.ResponseModelInterface.Success;
        }

        public static async Task<IShowMatchResponseModel> ShowMatch(IRequestHeaders requestHeaders, int gameId)
        {
            var response =
                await new ShowMatchGetProcessor(requestHeaders, gameId).SendRequest(
                    "Matches data was retrieved successfully");
            return response.ResponseModelInterface;
        }

        public static async Task<IUpdateUserScoreResponseModel> MakeAMove(IRequestHeaders requestHeaders, int gameId)
        {
            var response =
                await new MakeAMovePostProcessor(requestHeaders, gameId).SendRequest(
                    "Player has made a move successfully");
            return response.ResponseModelInterface;
        }
    }
}