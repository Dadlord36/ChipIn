using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataModels.Interfaces
{
    public interface IChallengeBaseModel
    {
        [JsonProperty("challenge_type")] string ChallengeType { get; set; }

        [JsonProperty("started_at")]
        [JsonConverter(typeof(ServerFullDateTimeConverter))]
        DateTime StartedAt { get; set; }
    }

    internal class ServerFullDateTimeConverter : IsoDateTimeConverter
    {
        public ServerFullDateTimeConverter()
        {
            DateTimeFormat = "dd/MM/yyyy hh:mmtt";
        }
    }
}