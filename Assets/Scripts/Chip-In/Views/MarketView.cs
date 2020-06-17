using HttpRequests.RequestsProcessors.GetRequests;
using UnityEngine;
using Views.ViewElements;

namespace Views
{
    public sealed class MarketView : BaseView
    {
        [SerializeField] private RadarView radarView;

        public MarketView() : base(nameof(MarketView))
        {
        }

        public void SetRadarData(RadarData radarData)
        {
            radarView.SetDataToVisualize(radarData);
        }
    }
}