using System;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IDate
    {
        [JsonProperty("date")] DateTime Date { get; set; }
    }
}