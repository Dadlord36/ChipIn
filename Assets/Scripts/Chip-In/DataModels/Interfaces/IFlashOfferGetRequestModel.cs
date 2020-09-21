using Newtonsoft.Json;
using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface IPriceType
    {
        [JsonProperty("price_type")] string PriceType { get; set; }
    }

    public interface IPeriod
    {
        [JsonProperty("period")] string Period { get; set; }
    }

    public interface IFlashOfferGetRequestModel : ITitled, IDescription, IQuantity, IRadius, IPrice, IExpireDate, IPriceType, IPeriod
    {
    }
}