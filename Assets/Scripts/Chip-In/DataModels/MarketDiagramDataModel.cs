using DataModels.Interfaces;

namespace DataModels
{

    public class MarketDiagramDataModel : IMarketDiagramModel
    {
        public double Connection { get; set; }
        public double Acceptance { get; set; }
        public double Engagement { get; set; }
        public double Response { get; set; }
        public double Transaction { get; set; }
        public double Loyalty { get; set; }
    }
}