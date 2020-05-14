using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class CommunityInterestsResponseDataModel : ICommunityInterestsResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public CommunityInterestDataModel[] Interests { get; set; }
    }
}