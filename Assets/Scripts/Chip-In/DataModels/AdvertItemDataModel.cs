using System.Collections.Generic;
using DataModels.Interfaces;

namespace DataModels
{
    public class AdvertItemDataModel : IAdvertItemModel
    {
        public int? Id { get; set; }
        public string Slogan { get; set; }
        public string PosterUri { get; set; }
        public IList<AdvertFeatureDataModel> AdvertFeatures { get; set; }
    }
}