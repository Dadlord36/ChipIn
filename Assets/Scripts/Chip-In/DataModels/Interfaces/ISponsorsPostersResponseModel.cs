using System.Collections.Generic;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface ISponsorsPostersResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("posters")]
        IList<SponsoredPosterDataModel> SponsorPosters { get; set; }
    }
}