using DataModels.Interfaces;
using DataModels.SimpleTypes;
using Views;

namespace DataModels
{
    public class AdvertDataModel : IAdvertModel
    {
        public string Slogan { get; set; }
        public FilePath PosterFilePath { get; set; }
        public AdvertFeatureDataModel[] Features { get; set; }
    }
}