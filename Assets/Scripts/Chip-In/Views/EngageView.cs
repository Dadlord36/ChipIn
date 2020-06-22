using System;
using System.Threading.Tasks;
using DataModels;
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
            catch (ImagesDownloadingUtility.DataDownloadingFailureException e)
            {
                LogUtility.PrintLog(Tag, e.Message);
                return null;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}