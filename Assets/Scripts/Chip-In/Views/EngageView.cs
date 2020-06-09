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
    public class EngageView : BaseView
    {
        [SerializeField] private EngageCardViewModel prefab;
        [SerializeField] private Transform scrollViewContainer;

        public void ClearScrollList()
        {
            foreach (Transform child in scrollViewContainer)
            {
                Destroy(child.gameObject);
            }
        }

        public async Task<EngageCardViewModel> AddCardToScrollList(ICommunityDetailsDataModel communityDetailsDataModel)
        {
            try
            {
                var posterTexture = await ImagesDownloadingUtility.TryDownloadImageAsync(ApiHelper.DefaultClient, communityDetailsDataModel.PosterUri);
                var engageCardView = Instantiate(prefab, scrollViewContainer);

                engageCardView.FillCardWithData(new EngageCardDataModel(communityDetailsDataModel,
                    SpritesUtility.CreateSpriteWithDefaultParameters(posterTexture)));

                return engageCardView;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
    }
}