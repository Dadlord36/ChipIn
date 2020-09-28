using System;
using Repositories.Remote;
using RequestsStaticProcessors;
using Tasking;
using UnityEngine;
using Utilities;
using Views;

namespace ViewModels
{
    public class MarketViewModel : ViewsSwitchingViewModel
    {
        [SerializeField] private UserAuthorisationDataRepository authorisationDataRepository;

        private MarketView ThisView => View as MarketView;

        public MarketViewModel() : base(nameof(MarketViewModel))
        {
        }

        protected override async void OnBecomingActiveView()
        {
            base.OnBecomingActiveView();
            try
            {
                IsAwaitingProcess = true;
                var response = await ProfileDataStaticRequestsProcessor.GetMarketDiagramDataAsync(out OperationCancellationController
                    .TasksCancellationTokenSource, authorisationDataRepository).ConfigureAwait(false);

                if (!response.Success) return;

                var marketData = response.ResponseModelInterface.MarketDiagramData;

                TasksFactories.ExecuteOnMainThread(delegate { ThisView.SetRadarData(marketData.GetDiagramConsumableData); });
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintDefaultOperationCancellationLog(Tag);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
            finally
            {
                IsAwaitingProcess = false;
            }
        }
    }
}