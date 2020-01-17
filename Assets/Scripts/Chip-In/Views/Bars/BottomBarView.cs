using ScriptableObjects;
using ScriptableObjects.Comparators;
using UnityEngine;
using Views.ViewElements.ViewsSwitching;

namespace Views.Bars
{
    public class BottomBarView : SingleViewSwitching
    {
        [SerializeField] private ViewsComparisonContainer associativeViewsContainer;
        [SerializeField] private bool shouldAutoControlVisibility = true;
        protected override void SwitchTo(BaseView viewToSwitchTo)
        {
            if(!shouldAutoControlVisibility) return;
            
            if (associativeViewsContainer.ContainsView(viewToSwitchTo))
            {
                Show();
                SelectionOptionsDictionary[viewToSwitchTo.GetViewName].SelectAsOneOfGroup();
            }
            else
            {
                Hide();
            }
        }
    }
}