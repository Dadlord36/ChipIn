using Newtonsoft.Json.Converters;

namespace DataModels.DateTimeConverters
{
    internal class ServerFullDateTimeConverter : IsoDateTimeConverter
    {
        public ServerFullDateTimeConverter()
        {
            DateTimeFormat = "dd/MM/yyyy hh:mmtt";
        }
    }
}