using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public sealed class CommunityInterestLabelDataRequestResponse : ICommunityInterestLabelDataRequestResponse
    {
        public bool Success { get; set; }
        public PaginationData Pagination { get; set; }
        public CommunityInterestLabelData[] Communities { get; set; }

        public CommunityInterestLabelDataRequestResponse(bool success, PaginationData paginationData,
            CommunityInterestLabelData[] communities)
        {
            Success = success;
            Pagination = paginationData;
            Communities = communities;
        }
    }
}