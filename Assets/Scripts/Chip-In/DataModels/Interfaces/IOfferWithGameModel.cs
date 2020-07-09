using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IOfferWithGameModel : IOfferBaseModel, IIdentifier, IPosterImageUri
    {
        [JsonProperty("game")] GameDataModel GameData { get; set; }
    }
}