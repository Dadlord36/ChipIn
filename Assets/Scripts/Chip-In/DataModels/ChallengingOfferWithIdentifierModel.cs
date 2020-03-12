using System;
using DataModels.Interfaces;

namespace DataModels
{
    public sealed class ChallengingOfferWithIdentifierModel : IChallengingOfferWithIdentifier, IPosterImageUri
    {
        public string PosterUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Segment { get; set; }
        public uint Quantity { get; set; }
        public uint Price { get; set; }
        public int? Id { get; set; }
        public DateTime StartedAt { get; set; }
        public string ChallengeType { get; set; }
    }
}