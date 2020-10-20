using ScriptableObjects.SwitchBindings;
using UnityEngine.EventSystems;
using ViewModels.SwitchingControllers;
using ViewModels.UI.Elements;

namespace ViewModels
{
    public interface IViewsSwitchingMediator
    {
        void SwitchToView(string viewName, FormsTransitionBundle formsTransitionBundle, bool recreateViewToSwitchTo);
        void SwitchToView(string viewName, bool recreateViewToSwitchTo);
        void SwitchToView(string viewName);
        void SwitchToPreviousView(in FormsTransitionBundle formsTransitionBundle, bool recreateViewToSwitchTo);

        void SwitchToView(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle, bool recreateViewToSwitchTo);

        void SwitchToPreviousView();

        void SwitchToView(ViewsPairInfo viewsPairInfo, in ViewsSwitchingParameters defaultViewsSwitchingParameters, bool recreateViewToSwitchTo,
            FormsTransitionBundle formsTransitionBundle);
    }
    
    public class ViewsSwitchingMediator : IViewsSwitchingMediator
    {
        private readonly BaseViewSwitchingController _viewsSwitchingController;
        private readonly ViewsSwitchingAnimationBinding _viewsSwitchingAnimationBinding;
        private readonly string _viewName;

        private readonly ViewsSwitchingParameters _defaultSwitchingParameters = new ViewsSwitchingParameters
        (
            new ViewAppearanceParameters(ViewAppearanceParameters.Appearance.MoveOut, false,
                ViewAppearanceParameters.SwitchingViewPosition.Under, MoveDirection.Left, .5f),
            new ViewAppearanceParameters(ViewAppearanceParameters.Appearance.MoveIn, false,
                ViewAppearanceParameters.SwitchingViewPosition.Above, MoveDirection.Right)
        );

        public ViewsSwitchingMediator(BaseViewSwitchingController viewsSwitchingController, ViewsSwitchingAnimationBinding viewsSwitchingAnimationBinding,
            string viewName)
        {
            _viewsSwitchingController = viewsSwitchingController;
            _viewsSwitchingAnimationBinding = viewsSwitchingAnimationBinding;
            _viewName = viewName;
        }


        public void SwitchToView(string viewName)
        {
            SwitchToView(viewName, false);
        }

        public void SwitchToView(string viewName, FormsTransitionBundle formsTransitionBundle = default, bool recreateViewToSwitchTo = false)
        {
            SwitchToView(new ViewsPairInfo(null, viewName), formsTransitionBundle, recreateViewToSwitchTo);
        }

        public void SwitchToView(string viewName, bool recreateViewToSwitchTo)
        {
            SwitchToView(new ViewsPairInfo(null, viewName), default, recreateViewToSwitchTo);
        }

        public void SwitchToPreviousView(in FormsTransitionBundle formsTransitionBundle, bool recreateViewToSwitchTo)
        {
            _viewsSwitchingController.SwitchToPreviousView(formsTransitionBundle, recreateViewToSwitchTo);
            _viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(ReturnButton.DefaultParameters);
        }

        public void SwitchToView(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle = default,
            bool recreateViewToSwitchTo = false)
        {
            SwitchToViewCoroutine(viewsPairInfo, formsTransitionBundle, recreateViewToSwitchTo);
            /*TasksFactories.ExecuteOnMainThread(() =>
            {
                StartCoroutine();
            });*/
        }

        public void SwitchToPreviousView()
        {
            _viewsSwitchingController.SwitchToPreviousView();
            _viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(ReturnButton.DefaultParameters);
        }

        public void SwitchToView(ViewsPairInfo viewsPairInfo, in ViewsSwitchingParameters defaultViewsSwitchingParameters, bool recreateViewToSwitchTo,
            FormsTransitionBundle formsTransitionBundle = default)
        {
            InvokeViewsSwitching(viewsPairInfo, formsTransitionBundle, recreateViewToSwitchTo);
            _viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(defaultViewsSwitchingParameters);
        }

        private void InvokeViewsSwitching(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle, bool recreateViewToSwitchTo)
        {
            _viewsSwitchingController.RequestSwitchToView(string.IsNullOrEmpty(viewsPairInfo.ViewToSwitchFromName)
                ? _viewName
                : viewsPairInfo.ViewToSwitchFromName, viewsPairInfo.ViewToSwitchToName, recreateViewToSwitchTo, formsTransitionBundle);
        }

        private void SwitchToViewCoroutine(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle,
            bool recreateViewToSwitchTo)
        {
            InvokeViewsSwitching(viewsPairInfo, formsTransitionBundle, recreateViewToSwitchTo);
            _viewsSwitchingAnimationBinding.RequestViewsSwitchingAnimation(_defaultSwitchingParameters);
            /*yield return null;*/
        }
    }
}