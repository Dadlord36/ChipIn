using DataModels.Interfaces;

namespace DataModels
{
    public class AdvertFeatureDataModel : IAdvertFeatureModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public int TokensAmount { get; set; }
        public string Icon { get; set; }
        public bool Watched { get; set; }
    }
}