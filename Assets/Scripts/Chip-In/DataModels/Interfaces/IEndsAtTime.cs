using System;
using DataModels.DateTimeConverters;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IEndsAtTime
    {
        [JsonConverter(typeof(ServerFullDateTimeConverter))]
        [JsonProperty("ends_at")]
        DateTime EndsAtTime { get; set; }
    }
}