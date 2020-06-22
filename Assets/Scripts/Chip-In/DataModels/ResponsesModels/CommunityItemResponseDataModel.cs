using HttpRequests.RequestsProcessors.GetRequests;

namespace DataModels.ResponsesModels
{
    public class CommunityItemResponseDataModel : ICommunityItemResponseModel
    {
        public bool Success { get; set; }
        public MarketInterestDetailsDataModel LabelDetailsDataModel { get; set; }
    }
}