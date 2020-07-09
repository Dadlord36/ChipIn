using Newtonsoft.Json.Converters;
using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface IOfferBaseModel : ICategory, ITitled, IDescription, IMarketSegment, IQuantity, IPrice, IExpireDate
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