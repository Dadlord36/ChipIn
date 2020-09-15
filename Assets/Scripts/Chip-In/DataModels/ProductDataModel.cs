using DataModels.Interfaces;
using HttpRequests.RequestsProcessors.GetRequests;

namespace DataModels
{
    public class ProductDataModel : IProductModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PosterUri { get; set; }
        public string QrData { get; set; }
    }
}