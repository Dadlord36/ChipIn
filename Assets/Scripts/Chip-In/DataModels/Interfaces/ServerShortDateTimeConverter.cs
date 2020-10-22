using Newtonsoft.Json.Converters;
using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    internal class ServerShortDateTimeConverter : IsoDateTimeConverter
    {
        public ServerShortDateTimeConverter()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }
    }
}