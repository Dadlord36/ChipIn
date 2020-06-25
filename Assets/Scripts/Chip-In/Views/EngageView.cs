using System;
using System.Threading;
using System.Threading.Tasks;
using DataModels;
using DataModels.Interfaces;
using HttpRequests;
using UnityEngine;
using Utilities;
using ViewModels.Cards;
using WebOperationUtilities;

namespace Views
{
    public sealed class EngageView : BaseView
    {
        [SerializeField] private EngageCardViewModel prefab;
        [SerializeField] private Transform scrollViewContainer;

        public EngageView() : base(nameof(EngageView))
        {
        }

        public void ClearScrollList()
        {
            foreach (Transform child in scrollViewContainer)
            {
                Destroy(child.gameObject);
            }
        }

        public async Task<EngageCardViewModel> AddCardToScrollList(IMarketInterestDetailsDataModel marketInterestDetailsDataModel,
            CancellationToken cancellationToken)
        {
            try
            {
                var posterTexture = await ImagesDownloadingUtility.CreateDownloadImageTask(ApiHelper.DefaultClient,
                    TaskScheduler.FromCurrentSynchronizationContext(), marketInterestDetailsDataModel.PosterUri, cancellationToken);
                var engageCardView = Instantiate(prefab, scrollViewContainer);

                engageCardView.FillCardWithData(new EngageCardDataModel(marketInterestDetailsDataModel,
                    SpritesUtility.CreateSpriteWithDefaultParameters(posterTexture)));

                return engageCardView;
            }
            catch (OperationCanceledException e)
            {
                LogUtility.PrintLog(Tag, e.Message);
                throw;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}