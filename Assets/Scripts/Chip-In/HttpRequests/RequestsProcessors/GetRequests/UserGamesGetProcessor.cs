using System.Collections.Generic;
using System.Net.Http;
using DataModels;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class UserGamesGetProcessor : RequestWithoutBodyProcessor<UserGamesResponseModel, IUserGamesResponseModel>
    {
        public UserGamesGetProcessor(IRequestHeaders requestHeaders) : base(RequestsSuffixes.UserGames, HttpMethod.Get,
            requestHeaders, null)
        {
        }
    }

    public class JoinGamePostProcessor : RequestWithoutBodyProcessor<SuccessConfirmationModel, ISuccess>
    {
        public JoinGamePostProcessor(IRequestHeaders requestHeaders, IReadOnlyList<string> requestParameters) :
            base(RequestsSuffixes.Games, HttpMethod.Post, requestHeaders, requestParameters)
        {
        }
    }

    public class
        MakeAMovePostProcessor : RequestWithoutBodyProcessor<UpdateUserScoreResponseModel, IUpdateUserScoreResponseModel>
    {
        public MakeAMovePostProcessor( IRequestHeaders requestHeaders, int gameId) 
            : base(RequestsSuffixes.Games, HttpMethod.Post, requestHeaders, new []
            {
                gameId.ToString(), GameRequestParameters.Match,GameRequestParameters.Move
            })
        {
        }
    }
}