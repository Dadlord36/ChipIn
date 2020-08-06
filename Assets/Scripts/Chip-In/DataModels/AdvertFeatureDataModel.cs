using DataModels.Interfaces;

namespace DataModels
{
    public class AdvertFeatureDataModel : IAdvertFeatureModel
    {
        public int? Id { get; set; }
        public string Description { get; set; }
        public uint TokensAmount { get; set; }
        public string Icon { get; set; }
        public bool Watched { get; set; }
    }
}