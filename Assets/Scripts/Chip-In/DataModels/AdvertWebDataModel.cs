using DataModels.Interfaces;
using DataModels.SimpleTypes;
using Views;

namespace DataModels
{
    public class AdvertWebDataModel : IAdvertWebModel
    {
        public int? Id { get; set; }
        public string Slogan { get; set; }
        public FilePath PosterFilePath { get; set; }
        public AdvertFeatureDataModel[] Features { get; set; }
    }
}