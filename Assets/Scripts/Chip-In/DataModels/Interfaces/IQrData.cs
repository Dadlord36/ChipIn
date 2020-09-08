using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IQrData
    {
      [JsonProperty("qr_data")] string QrData { get; set; }
    }
}