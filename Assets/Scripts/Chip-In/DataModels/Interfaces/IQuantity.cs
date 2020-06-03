using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IQuantity
    {
        [JsonProperty("quantity")] uint Quantity { get; set; }
    }
}