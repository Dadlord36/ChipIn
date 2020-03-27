using HttpRequests.RequestsProcessors.GetRequests;
using UnityEngine;
using Views.ViewElements;

namespace Views
{
    public class MarketView : BaseView
    {
        [SerializeField] private RadarView radarView;
        public void SetRadarData(RadarData radarData)
        {
            radarView.SetDataToVisualize(radarData);
        }
    }
}