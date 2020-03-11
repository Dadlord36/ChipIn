using System.Net.Http;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class ShowMatchGetProcessor : RequestWithoutBodyProcessor<ShowMatchResponseModel, IShowMatchResponseModel>
    {
        public ShowMatchGetProcessor(IRequestHeaders requestHeaders, int gameId) : base(ApiCategories.Games,
            HttpMethod.Get, requestHeaders, new[] {gameId.ToString(), GameRequestParameters.Match})
        {
        }
    }
}