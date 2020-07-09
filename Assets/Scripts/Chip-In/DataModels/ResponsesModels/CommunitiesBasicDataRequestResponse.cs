using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public sealed class CommunitiesBasicDataRequestResponse : ICommunitiesBasicDataRequestResponse
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public InterestBasicDataModel[] Communities { get; set; }
    }
}