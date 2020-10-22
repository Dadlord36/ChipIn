using Newtonsoft.Json;
using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface IRealPrice
    {
        [JsonProperty("real_price")] uint RealPrice { get; set; }
    }

    public interface IFundPrice
    {
        [JsonProperty("fund_price")] uint FundPrice { get; set; }
    }

    public interface IOffer : IPosterImageUri, IExpireDate, IIdentifier, ITitled, IDescription, IQuantity, ICategory, IPrice,
        IRealPrice, IPriceType, IProductCategory
    {
        [JsonProperty("interest")] InterestBasicDataModel Interest { get; set; }
    }

    public interface IClientOfferModel : IOffer
    {
    }

    public interface IOwnerOfferModel : IOffer, IFundPrice
    {
    }
}