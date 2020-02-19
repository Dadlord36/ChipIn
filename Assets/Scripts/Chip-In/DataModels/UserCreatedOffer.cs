using System;
using DataModels.Interfaces;
using DataModels.SimpleTypes;
using Newtonsoft.Json;

namespace DataModels
{
    public sealed class UserCreatedOffer : IChallengingOffer
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Segment { get; set; }
        public uint Quantity { get; set; }
        public uint Price { get; set; }
        public string ChallengeType { get; set; }
        public DateTime StartedAt { get; set; }
    }
}