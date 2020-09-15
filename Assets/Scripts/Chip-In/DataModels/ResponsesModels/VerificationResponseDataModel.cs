using System.Collections.Generic;
using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class VerificationResponseDataModel : IVerificationResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public IList<VerificationDataModel> Verification { get; set; }
    }
}