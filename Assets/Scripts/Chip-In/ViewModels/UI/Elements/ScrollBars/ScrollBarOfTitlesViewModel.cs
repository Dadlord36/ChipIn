using System.Collections.Generic;
using System.Collections.Specialized;
using ScriptableObjects.DataSets;
using UnityEngine;
using Views.Bars.BarItems;

namespace ViewModels.UI.Elements.ScrollBars
{
    public class ScrollBarOfTitlesViewModel : BaseScrollBar<ScrollBarItemWithTextView>
    {
        [SerializeField] private bool shouldFillFromContainer;

        private void Start()
        {
            if (shouldFillFromContainer)
                FillContainerWithItems();
            else
            {
                Initialize();
            }
        }

        public void UpdateContent(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                RemoveScrollBarItems();
            }
            else
            {
                FillWithItems(e.NewItems as List<string>);
            }
        }

        private void FillWithItems(IReadOnlyList<string> items)
        {
            var itemsData = new ScrollBarItemData[items.Count];

            for (var i = 0; i < items.Count; i++)
            {
                itemsData[i] = new ScrollBarItemData {Id = i, Title = items[i]};
            }

            scrollBarElementsData.ItemsData = itemsData;

            FillContainerWithItems();
        }
    }
}