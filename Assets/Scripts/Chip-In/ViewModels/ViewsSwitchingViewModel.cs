using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.EventSystems;
using ViewModels.Basic;
using ViewModels.SwitchingControllers;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel
    {
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;
        [SerializeField] private ViewsSwitchingAnimationBinding viewsSwitchingAnimationBinding;

        private readonly ViewsSwitchingParameters _defaultSwitchingParameters = new ViewsSwitchingParameters
        (
            new ViewAppearanceParameters(ViewAppearanceParameters.Appearance.MoveOut, false,
                ViewAppearanceParameters.SwitchingViewPosition.Under, MoveDirection.Left, .5f),
            new ViewAppearanceParameters(ViewAppearanceParameters.Appearance.MoveIn, false,
                ViewAppearanceParameters.SwitchingViewPosition.Above, MoveDirection.Right)
        );

        private void InvokeViewsSwitching(ViewsPairInfo viewsPairInfo)
        {
            viewsSwitchingController.RequestSwitchToView(string.IsNullOrEmpty(viewsPairInfo.ViewToSwitchFromName)
                ? View.ViewName
                : viewsPairInfo.ViewToSwitchFromName, viewsPairInfo.ViewToSwitchToName);
        }

        protected void SwitchToView(ViewsPairInfo viewsPairInfo)
        {
            InvokeViewsSwitching(viewsPairInfo);
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(_defaultSwitchingParameters);
        }

        protected void SwitchToView(ViewsPairInfo viewsPairInfo, in ViewsSwitchingParameters defaultViewsSwitchingParameters)
        {
            InvokeViewsSwitching(viewsPairInfo);
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(defaultViewsSwitchingParameters);
        }

        public void SwitchToView(string viewName)
        {
            SwitchToView(new ViewsPairInfo(null, viewName));
        }

        public ViewsSwitchingViewModel(string tag) : base(tag)
        {
        }
    }
}