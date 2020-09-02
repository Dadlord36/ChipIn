using HttpRequests.RequestsProcessors.GetRequests;
using UnityEngine;
using Views.ViewElements;

namespace Views
{
    public sealed class MarketView : BaseView
    {
        [SerializeField] private SplineDiagram splineDiagram;

        public MarketView() : base(nameof(MarketView))
        {
        }

        public void SetRadarData(RadarData radarData)
        {
            splineDiagram.VisualizePoints(radarData.Points, radarData.Max);
        }

        public void SetRadarData(AngleAndDistancePercentage[] data)
        {
            splineDiagram.VisualizePoints(data);
        }
    }
}