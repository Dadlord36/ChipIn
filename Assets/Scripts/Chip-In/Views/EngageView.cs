using DataModels;
using UnityEngine;
using ViewModels.Cards;

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
                Destroy(child);
            }
        }

        public EngageCardViewModel AddCardToScrollList(EngageCardDataModel engageCardDataModel)
        { 
            var engageCardView = Instantiate(prefab, scrollViewContainer);
            engageCardView.FillCardWithData(engageCardDataModel);
            return engageCardView;
        }
    }
}