using System;
using DataModels.DateTimeConverters;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface ICreatedAtTime
    {
        [JsonProperty("created_at")]
        string CreatedAt { get; set; }
    }
}