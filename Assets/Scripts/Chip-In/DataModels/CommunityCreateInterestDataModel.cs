using System;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.SimpleTypes;
using Newtonsoft.Json;

namespace DataModels
{
    public interface ICommunityCreateInterestModel
    {
        [JsonProperty("interest")] InterestCreationData Interest { get; set; }
    }

    public sealed class InterestCreationData : IIsPublic, IMemberMessage, IMerchantMessage, IPosterImageFile,
        INamed, ISegmentIndex, IStartedAtTime, IActive

    {
        public bool IsPublic { get; set; }
        public string MemberMessage { get; set; }
        public string MerchantMessage { get; set; }
        public FilePath PosterFilePath { get; set; }
        public string Name { get; set; }
        public int SegmentIndex { get; set; }
        public DateTime StartedAt { get; set; }
        public bool Active { get; set; }

        [JsonProperty("user_interests_attributes")]
        private UserInterestAttribute[] UserAttributes { get; set; }
    }

    public sealed class CommunityCreateInterestDataModel : ICommunityCreateInterestModel
    {
        public InterestCreationData Interest { get; set; }
    }
}