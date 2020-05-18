using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IMerchantMessage
    {
        [JsonProperty("merchant_message")] string MerchantMessage { get; set; }
    }
}