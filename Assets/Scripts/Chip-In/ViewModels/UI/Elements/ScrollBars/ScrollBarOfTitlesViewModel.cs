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
    }
}