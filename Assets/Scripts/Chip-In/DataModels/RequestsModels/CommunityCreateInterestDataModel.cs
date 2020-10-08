using System;
using System.Collections.Generic;
using DataModels.Common;
using DataModels.Interfaces;
using DataModels.SimpleTypes;
using Newtonsoft.Json;

namespace DataModels.RequestsModels
{
    public interface ICommunityCreateInterestRequestBodyModel
    {
        [JsonProperty("interest")] InterestCreationDataModel Interest { get; set; }
    }

    public interface IInterestCreationModel : IIsPublic, IMemberMessage, IMerchantMessage, IPosterImageFile, INamed, ISegmentName, IStartedAtTime,
        IEndsAtTime
    {
        [JsonProperty("user_interests_attributes")]
        IList<UserInterestAttribute> UserAttributes { get; set; }
    }

    public sealed class InterestCreationDataModel : IInterestCreationModel
    {
        public bool IsPublic { get; set; }
        public string MemberMessage { get; set; }
        public string MerchantMessage { get; set; }
        public FilePath PosterFilePath { get; set; } = new FilePath("");
        public string Name { get; set; }
        public string SegmentName { get; set; }

        public DateTime StartedAt { get; set; }
        public DateTime EndsAtTime { get; set; }
        
        public IList<UserInterestAttribute> UserAttributes { get; set; }
    }

    public sealed class CommunityCreateInterestRequestBodyDataModel : ICommunityCreateInterestRequestBodyModel
    {
        public InterestCreationDataModel Interest { get; set; }
    }
}