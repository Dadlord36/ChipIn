using GlobalVariables;
using ScriptableObjects;
using ScriptableObjects.SwitchBindings;

namespace Views
{
    public class BottomBarView : MultiViewsSwitch
    {
        
        protected override void SwitchTo(MultiViewsSwitchingBinding.ViewsSwitchData viewsSwitchData)
        {
            var viewName = viewsSwitchData.toView.GetViewName;
            if (ViewsNames.IsMainView(viewName))
            {
                Show();
                selectionOptionsDictionary[viewName].SelectAsOneOfGroup();
            }
            else
                Hide();
        }
    }
}