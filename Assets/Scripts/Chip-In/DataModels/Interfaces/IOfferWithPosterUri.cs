using System;
using DataModels.SimpleTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DataModels.Interfaces
{
    public interface IOfferBaseModel
    {
        [JsonProperty("title")] string Title { get; set; }
        [JsonProperty("description")] string Description { get; set; }
        [JsonProperty("category")] string Category { get; set; }

        [JsonProperty("expired_at")]
        [JsonConverter(typeof(ServerShortDateTimeConverter))]
        DateTime ExpireDate { get; set; }

        [JsonProperty("segment")] string Segment { get; set; }
        [JsonProperty("quantity")] uint Quantity { get; set; }
        [JsonProperty("price")] uint Price { get; set; }
    }
    
    internal class ServerShortDateTimeConverter : IsoDateTimeConverter
    {
        public ServerShortDateTimeConverter()
        {
            DateTimeFormat = "dd/MM/yyyy";
        }
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