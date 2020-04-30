using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public sealed class CommunitiesBasicDataRequestResponse : ICommunitiesBasicDataRequestResponse
    {
        public bool Success { get; set; }
        public PaginationData Pagination { get; set; }
        public CommunityBasicDataModel[] Communities { get; set; }

        public CommunitiesBasicDataRequestResponse(bool success, PaginationData paginationData,
            CommunityBasicDataModel[] communities)
        {
            Success = success;
            Pagination = paginationData;
            Communities = communities;
        }
    }
}