using DataModels.Interfaces;

namespace DataModels
{
    public class SponsoredAdDataModel : ISponsoredAdModel
    {
        public string PosterUri { get; set; }

        public string LogoUrl { get; set; }
    }
}