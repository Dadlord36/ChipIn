using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class ShowMatchGetProcessor : RequestWithoutBodyProcessor<ShowMatchResponseModel, IShowMatchResponseModel>
    {
        public ShowMatchGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders, int gameId) :
            base(out cancellationTokenSource, ApiCategories.Games, HttpMethod.Get, requestHeaders,
                new[] {gameId.ToString(), GameRequestParameters.Match})
        {
        }
    }
}