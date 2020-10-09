using System.Collections;
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

        private void InvokeViewsSwitching(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle, bool recreateViewToSwitchTo)
        {
            viewsSwitchingController.RequestSwitchToView(string.IsNullOrEmpty(viewsPairInfo.ViewToSwitchFromName)
                ? View.ViewName
                : viewsPairInfo.ViewToSwitchFromName, viewsPairInfo.ViewToSwitchToName, recreateViewToSwitchTo, formsTransitionBundle);
        }

        protected void SwitchToView(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle = default,
            bool recreateViewToSwitchTo = false)
        {
            StartCoroutine(SwitchToViewCoroutine(viewsPairInfo, formsTransitionBundle, recreateViewToSwitchTo));
        }

        private IEnumerator SwitchToViewCoroutine(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle,
            bool recreateViewToSwitchTo)
        {
            InvokeViewsSwitching(viewsPairInfo, formsTransitionBundle, recreateViewToSwitchTo);
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(_defaultSwitchingParameters);
            yield return null;
        }

        protected void SwitchToView(ViewsPairInfo viewsPairInfo, in ViewsSwitchingParameters defaultViewsSwitchingParameters, bool recreateViewToSwitchTo,
            FormsTransitionBundle formsTransitionBundle = default)
        {
            InvokeViewsSwitching(viewsPairInfo, formsTransitionBundle, recreateViewToSwitchTo);
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(defaultViewsSwitchingParameters);
        }

        public void SwitchToView(string viewName, FormsTransitionBundle formsTransitionBundle = default, bool recreateViewToSwitchTo = false)
        {
            SwitchToView(new ViewsPairInfo(null, viewName), formsTransitionBundle, recreateViewToSwitchTo);
        }

        public void SwitchToView(string viewName, bool recreateViewToSwitchTo)
        {
            SwitchToView(new ViewsPairInfo(null, viewName), default, recreateViewToSwitchTo);
        }

        public void SwitchToView(string viewName)
        {
            SwitchToView(viewName, false);
        }

        protected void SwitchToPreviousView()
        {
            viewsSwitchingController.SwitchToPreviousView();
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(ReturnButton.DefaultParameters);
        }

        protected void SwitchToPreviousView(in FormsTransitionBundle formsTransitionBundle, bool recreateViewToSwitchTo)
        {
            viewsSwitchingController.SwitchToPreviousView(formsTransitionBundle, recreateViewToSwitchTo);
            viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(ReturnButton.DefaultParameters);
        }
    }
}