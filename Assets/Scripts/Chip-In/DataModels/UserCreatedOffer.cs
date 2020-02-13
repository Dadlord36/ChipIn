using System;
using DataModels.Interfaces;

namespace DataModels
{
    public sealed class UserCreatedOffer : IUserCreatedOffer
    {
        public string PosterUri { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public string ExpireDate { get; set; }
        public string Segment { get; set; }
        public uint Quantity { get; set; }
        public uint Price { get; set; }
        public string ChallengeType { get; set; }
        public DateTime StartedAt { get; set; }
        public OfferCreatorDataModel User { get; set; }
    }
}