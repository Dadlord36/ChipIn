using UnityEngine;
using ViewModels.SwitchingControllers;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel
    {
        protected const string SwitchingViewTag = "ViewsSwitching";

        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;

        protected void SwitchToView(string toViewName, string fromView = null)
        {
            viewsSwitchingController.RequestSwitchToView(string.IsNullOrEmpty(fromView) ? View.ViewName : fromView,
                toViewName);
        }

        public void SwitchToPreviousView()
        {
            viewsSwitchingController.SwitchToPreviousView();
        }
    }
}