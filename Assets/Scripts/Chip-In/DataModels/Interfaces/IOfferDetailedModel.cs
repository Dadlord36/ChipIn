using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IOfferDetailedModel : IOfferBaseData 
    {
        [JsonProperty("game")] GameDataModel GameData { get; set; }
    }
}