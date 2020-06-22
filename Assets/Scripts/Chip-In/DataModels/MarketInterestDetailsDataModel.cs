using DataModels.Interfaces;
using Newtonsoft.Json;

namespace DataModels
{
    [JsonObject(MemberSerialization.OptIn)]
    public class MarketInterestDetailsDataModel : InterestBasicDataModel, IMarketInterestDetailsDataModel
    {
        public bool IsValid => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(PosterUri);
        public string Description { get; set; }
        public uint Size { get; set; }
        public uint MinCap { get; set; }
        public uint MaxCap { get; set; }
        public string Age { get; set; }
        public string Spirit { get; set; }
    }
}