using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IOfferWithGameModel : IOfferBaseModel, IIdentifier
    {
        [JsonProperty("game")] GameModelModel Game { get; set; }
    }
}