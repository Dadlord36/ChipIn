using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class InterestsPagesPagesResponseDataModel : IInterestsPagesResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public InterestPagePageDataModel[] Interests { get; set; }
    }
}