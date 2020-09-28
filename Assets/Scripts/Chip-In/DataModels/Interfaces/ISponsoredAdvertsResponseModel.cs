using System.Collections.Generic;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface ISponsoredAdvertsResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("sponsored_adverts")]
        IList<SponsoredAdDataModel> SponsoredAdverts { get; set; }
    }
}