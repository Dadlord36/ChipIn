using System;
using DataModels.Interfaces;

namespace DataModels
{
    public class OwnerOfferDataModel : IOwnerOfferModel
    {
        public string PosterUri { get; set; }
        public DateTime ExpireDate { get; set; }
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint Quantity { get; set; }
        public string Category { get; set; }
        public uint Price { get; set; }
        public uint RealPrice { get; set; }
        public string PriceType { get; set; }
        public string ProductCategory { get; set; }
        public InterestBasicDataModel Interest { get; set; }
        public uint FundPrice { get; set; }
    }
}