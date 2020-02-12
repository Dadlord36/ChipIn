using ScriptableObjects.Comparators;
using UnityEngine;
using Views.ViewElements.ViewsSwitching;

namespace Views.Bars
{
    public class BottomBarView : SingleViewSwitching
    {
        [SerializeField] private ViewsComparisonContainer associativeViewsContainer;
        [SerializeField] private bool shouldAutoControlVisibility = true;
        [SerializeField] private bool highlightCorrespondingButtonOnViewSwitching = true;

        private string _currentViewName;

        public string CurrentViewName
        {
            get => _currentViewName;
        }

        protected override void SwitchTo(BaseView viewToSwitchTo)
        {
            if (!associativeViewsContainer.ContainsView(viewToSwitchTo))
            {
                if (shouldAutoControlVisibility)
                    Hide();
                return;
            }

            if (shouldAutoControlVisibility)
            {
                Show();
                _currentViewName = viewToSwitchTo.ViewName;
            }

            if (highlightCorrespondingButtonOnViewSwitching)
                SelectionOptionsDictionary[viewToSwitchTo.ViewName].PerformGroupActionWithoutNotification();
        }
    }
}