using System;
using DataModels.DateTimeConverters;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IDate
    {
        [JsonConverter(typeof(ServerFullDateTimeConverter))]
        [JsonProperty("date")] DateTime Date { get; set; }
    }
}