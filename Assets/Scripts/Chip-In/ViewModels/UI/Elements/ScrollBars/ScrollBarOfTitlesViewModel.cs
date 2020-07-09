using Views.Bars.BarItems;

namespace ViewModels.UI.Elements.ScrollBars
{
    public class ScrollBarOfTitlesViewModel : BaseScrollBar<ScrollBarItemWithTextView>
    {
        private void Start()
        {
            FillContainerWithItems();
        }
    }
}