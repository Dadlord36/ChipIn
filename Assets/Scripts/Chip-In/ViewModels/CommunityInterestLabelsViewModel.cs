using UnityWeld.Binding;
using ViewModels.Basic;
using Views;

namespace ViewModels
{
    [Binding]
    public class CommunityInterestLabelsViewModel : BaseMenuViewModel<CommunityInterestGridItemView.CommunityInterestGridItemData>
    {
        [Binding]
        public void Button_StartAnInterest_OnClick()
        {
            
        }
        
        protected override void UpdateItems()
        {
            base.UpdateItems();
            var itemsData = itemsRemoteRepository.ItemsData;
            for (int i = 0; i < itemsData.Count; i++)
            {
                newItemsScrollView.AddElement(itemsData[i].IconTextureData);
            }
        }
    }
}