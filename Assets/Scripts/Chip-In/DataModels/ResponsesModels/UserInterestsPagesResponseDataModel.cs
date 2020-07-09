using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public sealed class UserInterestsPagesResponseDataModel : IUserInterestsPagesResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public UserInterestPageDataModel[] Interests { get; set; }
    }
}