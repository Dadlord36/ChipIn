using ScriptableObjects.SwitchBindings;
using UnityEngine;
using UnityEngine.EventSystems;
using ViewModels.Basic;
using ViewModels.SwitchingControllers;
using ViewModels.UI.Elements;

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

        public ViewsSwitchingViewModel(string tag) : base(tag)
        {
        }
        
        private void InvokeViewsSwitching(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle)
        {
            viewsSwitchingController.RequestSwitchToView(string.IsNullOrEmpty(viewsPairInfo.ViewToSwitchFromName)
                    ? View.ViewName
                    : viewsPairInfo.ViewToSwitchFromName, viewsPairInfo.ViewToSwitchToName, formsTransitionBundle);
        }

        protected void SwitchToView(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle = default)
        {
            InvokeViewsSwitching(viewsPairInfo, formsTransitionBundle);
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(_defaultSwitchingParameters);
        }

        protected void SwitchToView(ViewsPairInfo viewsPairInfo, in ViewsSwitchingParameters defaultViewsSwitchingParameters,
            FormsTransitionBundle formsTransitionBundle = default)
        {
            InvokeViewsSwitching(viewsPairInfo, formsTransitionBundle);
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(defaultViewsSwitchingParameters);
        }

        public void SwitchToView(string viewName, FormsTransitionBundle formsTransitionBundle = default)
        {
            SwitchToView(new ViewsPairInfo(null, viewName), formsTransitionBundle);
        }

        public void SwitchToView(string viewName)
        {
            SwitchToView(new ViewsPairInfo(null, viewName));
        }
        
        protected void SwitchToPreviousView()
        {
            viewsSwitchingController.SwitchToPreviousView();
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(ReturnButton.DefaultParameters);
        }

        protected void SwitchToPreviousView(in FormsTransitionBundle formsTransitionBundle)
        {
            viewsSwitchingController.SwitchToPreviousView(formsTransitionBundle);
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(ReturnButton.DefaultParameters);
        }
    }
}