using ScriptableObjects.SwitchBindings;
using UnityEngine;
using ViewModels.SwitchingControllers;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel
    {
        protected const string SwitchingViewTag = "ViewsSwitching";

        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;

        protected void SwitchToView(string toViewName, string fromView = null,
            ViewsSwitchData.AppearingSide viewAppearingSide = ViewsSwitchData.AppearingSide.FromRight)
        {
            viewsSwitchingController.RequestSwitchToView(string.IsNullOrEmpty(fromView) ? View.ViewName : fromView,
                toViewName, viewAppearingSide);
        }

        public void SwitchToView(string viewName)
        {
            SwitchToView(viewName,null);
        }
    }
}