using ScriptableObjects.Comparators;
using ScriptableObjects.SwitchBindings;
using UnityEngine;
using Views.ViewElements.ViewsSwitching;

namespace Views.Bars
{
    public class BottomBarView : MultiViewsSwitch
    {
        [SerializeField] private ViewsComparisonContainer associativeViewsContainer;
        [SerializeField] private bool shouldAutoControlVisibility = true;
        [SerializeField] private bool highlightCorrespondingButtonOnViewSwitching = true;

        public string CurrentViewName { get; private set; }

        protected override void SwitchTo(ViewsSwitchData viewsSwitchData)
        {
            if (!associativeViewsContainer.ContainsView(viewsSwitchData.ViewToSwitchTo))
            {
                if (shouldAutoControlVisibility)
                    Hide();
                return;
            }

            if (shouldAutoControlVisibility)
            {
                Show();
                CurrentViewName = viewsSwitchData.ViewToSwitchTo.ViewName;
            }

            if (highlightCorrespondingButtonOnViewSwitching)
                SelectionOptionsDictionary[viewsSwitchData.ViewToSwitchTo.ViewName]
                    .PerformGroupAction();
        }
    }
}