using Newtonsoft.Json.Converters;
using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface IOfferBaseModel : ICategory, ITitled, IDescription, IQuantity, IPrice, IExpireDate
    {
    }

    public interface IOfferModel : IOfferBaseModel, IPosterImageUri, IIdentifier, IPriceType
    {
        
    }

    internal class ServerShortDateTimeConverter : IsoDateTimeConverter
    {
        public ServerShortDateTimeConverter()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }
    }
}