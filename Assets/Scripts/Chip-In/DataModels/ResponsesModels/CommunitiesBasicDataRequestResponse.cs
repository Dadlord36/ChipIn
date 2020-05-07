using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public sealed class CommunitiesBasicDataRequestResponse : ICommunitiesBasicDataRequestResponse
    {
        public bool Success { get; set; }
        public PaginatedResponseData PaginatedResponse { get; set; }
        public CommunityBasicDataModel[] Communities { get; set; }

        public CommunitiesBasicDataRequestResponse(bool success, PaginatedResponseData paginatedResponseData,
            CommunityBasicDataModel[] communities)
        {
            Success = success;
            PaginatedResponse = paginatedResponseData;
            Communities = communities;
        }
    }
}