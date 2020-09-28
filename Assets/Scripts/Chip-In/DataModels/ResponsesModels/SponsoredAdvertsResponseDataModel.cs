using System.Collections.Generic;
using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class SponsoredAdvertsResponseDataModel : ISponsoredAdvertsResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public IList<SponsoredAdDataModel> SponsoredAdverts { get; set; }
    }
}