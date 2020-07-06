using HttpRequests.RequestsProcessors.GetRequests;

namespace DataModels.ResponsesModels
{
    public class InterestDetailsResponseDataModel : IInterestDetailsResponseModel
    {
        public bool Success { get; set; }
        public MarketInterestDetailsDataModel LabelDetailsDataModel { get; set; }
    }
}