using System;
using HttpRequests.RequestsProcessors.GetRequests;
using Repositories.Remote;
using RequestsStaticProcessors;
using UnityEngine;
using Utilities;
using Views;

namespace ViewModels
{
    public class MarketViewModel : ViewsSwitchingViewModel
    {
        private const string Tag = nameof(MarketplaceViewModel);

        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;
        /*[SerializeField] private Vector2[] positions;
        [SerializeField] private float max = 10f;*/
        private MarketView ThisView => View as MarketView;

        protected override void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            TryUpdateRadarViewData();
        }

        protected override void OnBecomingInactiveView()
        {
            base.OnBecomingInactiveView();
        }

        /*private void DebugPoints()
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
        }*/

        /*private void Update()
        {
            DebugPoints();
        }*/

        private async void TryUpdateRadarViewData()
        {
            try
            {
                var response = await MerchantMarketRequestsStaticProcessor.GetRadarData(authorisationDataRepository);
                if (!response.Success) return;

                var responseModel = response.ResponseModelInterface;
                if (responseModel.Data.Points == null)
                {
                    LogUtility.PrintLog(Tag, "There are no points were returned");
                    return;
                }

                ThisView.SetRadarData(responseModel.Data);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}