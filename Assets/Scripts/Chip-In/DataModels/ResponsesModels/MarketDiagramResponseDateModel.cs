using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class MarketDiagramResponseDateModel : IMarketDiagramResponseModel
    {
        public bool Success { get; set; }
        public MarketDiagramDataModel MarketDiagramData { get; set; }
    }
}