using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public sealed class MarketInterestsDetailsDataRequestResponse : IMarketInterestsDetailsDataRequestResponse
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public MarketInterestDetailsDataModel[] MarketInterestsDetails { get; set; }
    }
}