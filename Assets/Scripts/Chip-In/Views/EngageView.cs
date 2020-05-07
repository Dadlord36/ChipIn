using System.Threading.Tasks;
using DataModels;
using UnityEngine;
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
            var posterTexture = await ImagesDownloadingUtility.TryDownloadImageAsync(communityDetailsDataModel.PosterUri);
            var engageCardView = Instantiate(prefab, scrollViewContainer);
            
            engageCardView.FillCardWithData(new EngageCardDataModel(communityDetailsDataModel,
                SpritesUtility.CreateSpriteWithDefaultParameters(posterTexture)));

            return engageCardView;
        }
    }
}