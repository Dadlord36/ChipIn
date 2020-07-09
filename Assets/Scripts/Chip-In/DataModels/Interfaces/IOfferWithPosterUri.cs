using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface IOfferBaseModel : ICategory, ITitled, IDescription, IMarketSegment, IQuantity, IPrice
    {
        [JsonProperty("expired_at")]
        [JsonConverter(typeof(ServerShortDateTimeConverter))]
        DateTime ExpireDate { get; set; }
    }

    internal class ServerShortDateTimeConverter : IsoDateTimeConverter
    {
        public ServerShortDateTimeConverter()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }
    }
}