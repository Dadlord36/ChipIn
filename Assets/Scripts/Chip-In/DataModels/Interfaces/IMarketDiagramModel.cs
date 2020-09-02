using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IMarketDiagramModel
    {
        [JsonProperty("connection")]
        public double Connection { get; set; }

        [JsonProperty("acceptance")]
        public double Acceptance { get; set; }

        [JsonProperty("engagement")]
        public double Engagement { get; set; }

        [JsonProperty("response")]
        public double Response { get; set; }

        [JsonProperty("transaction")]
        public double Transaction { get; set; }

        [JsonProperty("loyalty")]
        public double Loyalty { get; set; }
    }
}