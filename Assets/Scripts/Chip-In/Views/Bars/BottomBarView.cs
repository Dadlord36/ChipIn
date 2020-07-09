using ScriptableObjects.Comparators;
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

        protected override void SwitchTo(BaseView baseView)
        {
            if (!associativeViewsContainer.ContainsView(baseView))
            {
                if (shouldAutoControlVisibility)
                    Hide();
                return;
            }

            if (shouldAutoControlVisibility)
            {
                Show();
            }

            _viewSwitchingListener?.OnViewSwitched(baseView.ViewName);

            if (highlightCorrespondingButtonOnViewSwitching)
                SelectionOptionsDictionary[baseView.ViewName]
                    .PerformGroupAction();
        }
    }
}