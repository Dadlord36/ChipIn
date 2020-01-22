using DataModels.Interfaces;

namespace DataModels
{
    public interface IOfferBaseModel : IOfferBaseData, IChallengeBaseData
    {
    }

    public sealed class OfferBasicModel : IOfferBaseModel
    {
        public string ChallengeType { get; set; }
        public string StartedAt { get; set; }
        public string PosterUri { get; set; }
        public string ExpireDate { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public uint Quantity { get; set; }
        public string Category { get; set; }
        public string Segment { get; set; }
        public uint Price { get; set; }
    }
}