using GlobalVariables;
using Views.ViewElements.ViewsSwitching;

namespace Views
{
    public class BottomBarView : SingleViewSwitching
    {

        protected override void SwitchTo(BaseView viewToSwitchTo)
        {
            var viewName = viewToSwitchTo.GetViewName;
            if (ViewsNames.IsBottomBarActiveView(viewName))
            {
                Show();
                SelectionOptionsDictionary[viewName].SelectAsOneOfGroup();
            }
            else
                Hide();
        }
    }
}