using UnityEngine;
using ViewModels.Interfaces;
using ViewModels.SwitchingControllers;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel
    {
        protected const string SwitchingViewTag = "ViewsSwitching";

        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;

        protected void SwitchToView(in string viewName)
        {
            viewsSwitchingController.RequestSwitchToView(viewName);
        }

        public void SwitchToPreviousView()
        {
            viewsSwitchingController.SwitchToPreviousView();
        }
    }
}