using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IUserCreatedOffer : IChallengingOffer
    {
        [JsonProperty("user")] OfferCreatorDataModel User { get; set; }
    }
}