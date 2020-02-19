using System;
using DataModels.SimpleTypes;
using Newtonsoft.Json;

namespace DataModels.Interfaces
{
    public interface IOfferBaseModel
    {
        [JsonProperty("title")] string Title { get; set; }
        [JsonProperty("description")] string Description { get; set; }
        [JsonProperty("category")] string Category { get; set; }

        [JsonProperty("expired_at")]
        [JsonConverter(typeof(ServerDateTimeConverter))]
        DateTime ExpireDate { get; set; }

        [JsonProperty("segment")] string Segment { get; set; }
        [JsonProperty("quantity")] uint Quantity { get; set; }
        [JsonProperty("price")] uint Price { get; set; }
    }

    public interface IPosterImageUri
    {
        [JsonProperty("poster")] string PosterUri { get; set; }
    }

    public interface IPosterImageFile
    {
        [JsonProperty("poster")] FilePath PosterFilePath { get; set; }
    }

    /*public interface IOfferWithPosterImageUri : IOfferBaseModel, IPosterImageUri
    {
    }

    public interface IOfferWithPosterImageFile : IOfferBaseModel, IPosterImageFile
    {
        
    }*/
}