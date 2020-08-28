using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Remote;
using UnityEngine;
using Views;

namespace ViewModels
{
    public class MarketViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        [SerializeField] private Vector2[] positions;
        [SerializeField] private float max = 10f;
        private MarketView ThisView => View as MarketView;

        public MarketViewModel() : base(nameof(MarketViewModel))
        {
        }

        private void DebugPoints()
        {
            if (positions == null || positions.Length == 0) return;

            var array = new float[positions.Length, 2];

            for (var index = 0; index < positions.Length; index++)
            {
                var position = positions[index];
                array[index, 0] = position.x;
                array[index, 1] = position.y;
            }

            ThisView.SetRadarData(new RadarData {Max = max, Points = array});
        }

        private void Start()
        {
            DebugPoints();
        }
    }
}