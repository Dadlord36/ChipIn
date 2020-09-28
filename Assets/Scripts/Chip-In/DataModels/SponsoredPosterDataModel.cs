using DataModels.Interfaces;

namespace DataModels
{
    public class SponsoredPosterDataModel : ISponsorPosterModel
    {
        public int? Id { get; set; }
        public string BackgroundUrl { get; set; }
        public string LogoUrl { get; set; }
    }
}