using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IOfferBaseData
    {
        [JsonProperty("poster")] string PosterUri { get; set; }
        [JsonProperty("expired_at")] string ExpireDate { get; set; }
        [JsonProperty("id")] int Id { get; set; }
        [JsonProperty("title")] string Title { get; set; }
        [JsonProperty("description")] string Description { get; set; }
        [JsonProperty("quantity")] uint Quantity { get; set; }
        [JsonProperty("category")] string Category { get; set; }
        [JsonProperty("segment")] string Segment { get; set; }
        [JsonProperty("price")] uint Price { get; set; }
    }
}