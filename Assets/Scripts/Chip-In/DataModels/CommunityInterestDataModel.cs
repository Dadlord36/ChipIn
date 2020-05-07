using System;
using DataModels.Interfaces;

namespace DataModels
{
    public class CommunityInterestDataModel : ICommunityInterestModel
    {
        public string Name { get; set; }
        public string Segment { get; set; }
        public int? Id { get; set; }
        public uint UsersCount { get; set; }
        public uint JoinedCount { get; set; }
        public string Message { get; set; }
        public string PosterUri { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime StartedAt { get; set; }
    }
}