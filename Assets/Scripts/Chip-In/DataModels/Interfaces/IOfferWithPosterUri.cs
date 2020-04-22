using System;
using DataModels.SimpleTypes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Views.Bars.BarItems;

namespace DataModels.Interfaces
{
    public interface ICategory
    {
        [JsonProperty("category")] string Category { get; set; }
    }
    
    public interface IOfferBaseModel : ICategory, ITitled, IDescription
    {
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