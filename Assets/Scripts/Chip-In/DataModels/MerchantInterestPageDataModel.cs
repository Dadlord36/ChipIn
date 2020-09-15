using System;
using DataModels.Interfaces;

namespace DataModels
{
    public sealed class MerchantInterestPageDataModel : InterestBasicDataModel, IInterestPageModel
    {
        public string Segment { get; set; }
        public uint UsersCount { get; set; }
        public uint JoinedCount { get; set; }
        public uint WatchedCount { get; set; }
        public uint SupportedCount { get; set; }
        public string FoundedCount { get; set; }
        public int TotalFound { get; set; }
        public string Message { get; set; }
        public string CreatedAt { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndsAtTime { get; set; }
    }
}