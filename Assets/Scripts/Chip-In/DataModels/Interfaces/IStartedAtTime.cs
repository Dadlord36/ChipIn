using System;
using DataModels.DateTimeConverters;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IStartedAtTime
    {
        [JsonConverter(typeof(ServerFullDateTimeConverter))]
        [JsonProperty("starts_at")]
        DateTime StartedAt { get; set; }
    }
}