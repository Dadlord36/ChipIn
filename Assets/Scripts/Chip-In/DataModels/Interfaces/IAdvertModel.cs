using Newtonsoft.Json;
using Views;

namespace DataModels.Interfaces
{
    public interface IAdvertModel : ISlogan, IPosterImageFile
    {
        [JsonProperty("advert_features_attributes")]
        AdvertFeatureDataModel[] Features { get; set; }
    }
}