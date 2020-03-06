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

        private INotifyViewSwitching _viewSwitchingListener;

        public void SetViewsSwitchingListener(INotifyViewSwitching listener)
        {
            _viewSwitchingListener = listener;
        }

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
            }

            _viewSwitchingListener?.OnViewSwitched(viewsSwitchData.ViewToSwitchTo.ViewName);

            if (highlightCorrespondingButtonOnViewSwitching)
                SelectionOptionsDictionary[viewsSwitchData.ViewToSwitchTo.ViewName]
                    .PerformGroupAction();
        }
    }
}