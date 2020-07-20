using System.Collections.Generic;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace DataModels.Interfaces
{
    public interface IAdvertsListResponseModel : ISuccess, IPaginatedResponse
    {
        [JsonProperty("adverts")]
        public IList<AdvertItemDataModel> Adverts { get; set; }
    }
}