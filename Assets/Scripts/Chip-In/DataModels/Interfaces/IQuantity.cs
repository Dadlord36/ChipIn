using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IQuantity
    {
        [JsonProperty("quantity")] int Quantity { get; set; }
    }
}