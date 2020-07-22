using System.Collections.Generic;
using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class AdvertsListResponseDataModel : IAdvertsListResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public IList<AdvertItemDataModel> Adverts { get; set; }
    }
}