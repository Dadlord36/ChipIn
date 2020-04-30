using DataModels.Interfaces;
using Newtonsoft.Json;

namespace DataModels
{

    public interface ICommunityBasicModel :INamed , IPosterImageUri, IIdentifier
    {
    }
    
    public class CommunityBasicDataModel : ICommunityBasicModel
    {
        public string Name { get; set; }
        public string PosterUri { get; set; }
        public int? Id { get; set; }
    }
    
    public interface ICommunityDetailsDataModel : ICommunityBasicModel, IDescription, IMarketModel
    {
    }

    [JsonObject(MemberSerialization.OptIn)]
    public class CommunityDetailsDataModel : ICommunityDetailsDataModel
    {
        public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(PosterUri);
        public int? Id { get; set; }
        public string Name { get; set; }
        public string PosterUri { get; set; }
        public string Description { get; set; }
        public uint Size { get; set; }
        public uint MinCap { get; set; }
        public uint MaxCap { get; set; }
        public string Age { get; set; }
        public string Spirit { get; set; }
    }
}