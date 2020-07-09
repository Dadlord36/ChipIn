using DataModels.Interfaces;

namespace DataModels
{
    public class AdvertFeatureDataModel : IAdvertFeatureModel
    {
        public string Description { get; set; }
        public int TokensAmount { get; set; }

        public string Icon { get; set; }
    }
}