using System.Collections.Generic;
using DataModels.Common;
using DataModels.Interfaces;

namespace DataModels.ResponsesModels
{
    public class SponsorsPostersResponseDataModel : ISponsorsPostersResponseModel
    {
        public bool Success { get; set; }
        public PaginatedResponseData Paginated { get; set; }
        public IList<SponsoredPosterDataModel> SponsorPosters { get; set; }
    }
}