using ScriptableObjects.SwitchBindings;
using UnityEngine;
using ViewModels.Basic;
using ViewModels.SwitchingControllers;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel
    {
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

        public ViewsSwitchingViewModel(string tag) : base(tag)
        {
        }
    }
}