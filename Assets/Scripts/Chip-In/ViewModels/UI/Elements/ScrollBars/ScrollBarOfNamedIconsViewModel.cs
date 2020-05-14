using Views.Bars.BarItems;

namespace ViewModels.UI.Elements.ScrollBars
{
    public class ScrollBarOfNamedIconsViewModel : BaseScrollBar<ScrollBarItemWithTitleAndIconView>
    {
        private void Start()
        {
            FillContainerWithItems();
        }
    }
}