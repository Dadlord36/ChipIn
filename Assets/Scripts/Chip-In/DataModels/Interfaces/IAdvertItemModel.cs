using System.Collections.Generic;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IAdvertItemModel : IIdentifier, ISlogan, IPosterImageUri
    {
        [JsonProperty("advert_features")] IList<AdvertFeatureDataModel> AdvertFeatures { get; set; }
    }
}