using Newtonsoft.Json;

namespace HttpRequests.RequestsProcessors.GetRequests
{
    public interface IQrData
    {
        [JsonProperty("qr_data")] string QrData { get; set; }
    }
}