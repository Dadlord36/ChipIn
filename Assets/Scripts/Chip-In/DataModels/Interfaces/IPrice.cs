using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IPrice
    {
        [JsonProperty("price")] uint Price { get; set; }
    }
}