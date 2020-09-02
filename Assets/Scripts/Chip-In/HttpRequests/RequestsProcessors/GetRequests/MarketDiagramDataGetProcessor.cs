using System.Net.Http;
using Common;
using DataModels.HttpRequestsHeadersModels;
using DataModels.Interfaces;
using DataModels.ResponsesModels;
using GlobalVariables;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public class MarketDiagramDataGetProcessor : RequestWithoutBodyProcessor<MarketDiagramResponseDateModel, IMarketDiagramResponseModel>
    {
        public MarketDiagramDataGetProcessor(out DisposableCancellationTokenSource cancellationTokenSource, IRequestHeaders requestHeaders) 
            : base(out cancellationTokenSource, ApiCategories.Profile, HttpMethod.Get, requestHeaders, new []{ApiCategories.Subcategories.Market})
        {
        }
    }
}