using System;
using DataModels.Interfaces;

namespace DataModels
{
    public class OfferWithGameModel : IOfferWithGameModel
    {
        public string PosterUri { get; set; }
        public DateTime ExpireDate { get; set; }
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint Quantity { get; set; }
        public string Category { get; set; }
        public string Segment { get; set; }
        public uint Price { get; set; }
        public GameDataModel GameData { get; set; }
    }
}