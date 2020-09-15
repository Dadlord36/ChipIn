using DataModels.Interfaces;
using Views.ViewElements;

namespace DataModels
{
    public class MarketDiagramDataModel : IMarketDiagramModel
    {
        public float Transaction { get; set; }
        public float Acceptance { get; set; }
        public float Response { get; set; }
        public float Loyalty { get; set; }
        public float Connection { get; set; }
        public float Engagement { get; set; }

        public const float ScaleFactor = 100f;

        public AngleAndDistancePercentage[] GetDiagramConsumableData
        {
            get
            {
                return new[]
                {
                    new AngleAndDistancePercentage(45f, Engagement / ScaleFactor),
                    new AngleAndDistancePercentage(90f, Connection / ScaleFactor),
                    new AngleAndDistancePercentage(135f, Loyalty / ScaleFactor),
                    new AngleAndDistancePercentage(225f, Response / ScaleFactor),
                    new AngleAndDistancePercentage(270f, Acceptance / ScaleFactor),
                    new AngleAndDistancePercentage(315f, Transaction / ScaleFactor),
                };
            }
        }
    }
}