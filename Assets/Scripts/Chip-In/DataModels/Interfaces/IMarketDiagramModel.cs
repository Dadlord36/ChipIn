using Newtonsoft.Json;
using Views.ViewElements;

namespace DataModels.Interfaces
{
    [JsonObject(MemberSerialization.OptIn)]
    public interface IMarketDiagramModel
    {
        [JsonProperty("connection")] float Connection { get; set; }

        [JsonProperty("acceptance")] float Acceptance { get; set; }

        [JsonProperty("engagement")] float Engagement { get; set; }

        [JsonProperty("response")] float Response { get; set; }

        [JsonProperty("transaction")] float Transaction { get; set; }

        [JsonProperty("loyalty")] float Loyalty { get; set; }

        AngleAndDistancePercentage[] GetDiagramConsumableData { get; }
    }
}