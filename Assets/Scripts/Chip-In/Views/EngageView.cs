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

        public EngageCardViewModel AddCardToScrollList(CommunityInterestGridItemView.CommunityInterestGridItemData engageCardDataModel)
        {
            var engageCardView = Instantiate(prefab, scrollViewContainer);
            engageCardView.FillCardWithData(new EngageCardDataModel(engageCardDataModel.ItemName,
                string.Empty, string.Empty, string.Empty, string.Empty, string.Empty,
                SpritesUtility.CreateSpriteWithDefaultParameters(engageCardDataModel.IconTextureData)));

            return engageCardView;
        }
    }
}