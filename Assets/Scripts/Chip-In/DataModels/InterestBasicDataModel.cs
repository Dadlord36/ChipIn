using DataModels.Interfaces;

namespace DataModels
{
    public class InterestBasicDataModel : IInterestBasicModel
    {
        public string Name { get; set; }
        public string PosterUri { get; set; }
        public int? Id { get; set; }
    }
}