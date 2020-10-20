using System;
using DataModels.Interfaces;

namespace DataModels
{
    public interface ICreatedOfferModel : IOfferBaseModel,IMarketSegment
    {
    }

    public sealed class UserCreatedOffer : ICreatedOfferModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string Segment { get; set; }
        public uint Quantity { get; set; }
        public uint Price { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}