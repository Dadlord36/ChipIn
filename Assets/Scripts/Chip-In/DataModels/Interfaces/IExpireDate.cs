using System;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IExpireDate
    {
        [JsonProperty("expired_at")]
        [JsonConverter(typeof(ServerShortDateTimeConverter))]
        DateTime ExpireDate { get; set; }
    }
}