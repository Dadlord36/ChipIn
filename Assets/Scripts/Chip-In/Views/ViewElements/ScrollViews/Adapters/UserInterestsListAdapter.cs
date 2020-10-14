using DataModels;
using Repositories.Remote.Paginated;
using UnityEngine;
using UnityWeld.Binding;
using Views.ViewElements.ScrollViews.Adapters.BaseAdapters;

namespace Views.ViewElements.ScrollViews.Adapters
{
    [Binding]
    public class UserInterestsListAdapter : SelectableElementsPagesListAdapter<InterestsBasicDataPaginatedListRepository,
        InterestBasicDataModel>
    {
        [SerializeField] private InterestBasicDataModel[] defaultItems;

        protected override void OnItemsCleared()
        {
            base.OnItemsCleared();
            AddItemsAt(0, defaultItems);
        }

        
        //TODO: add translation of items text
        /*private void TranslateItems()
        {
            for (int i = 0; i < defaultItems.Length; i++)
            {
                defaultItems[i].Name = I2.Loc.Localize.CurrentLocalizeComponent.
            }
        }*/
    }
}