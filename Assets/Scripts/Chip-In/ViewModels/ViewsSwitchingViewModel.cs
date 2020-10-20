using ScriptableObjects.SwitchBindings;
using UnityEngine;
using ViewModels.Basic;
using ViewModels.SwitchingControllers;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel, IViewsSwitchingMediator
    {
        [SerializeField] private BaseViewSwitchingController viewsSwitchingController;
        [SerializeField] private ViewsSwitchingAnimationBinding viewsSwitchingAnimationBinding;
        private ViewsSwitchingMediator _viewsSwitchingMediator;

        protected ViewsSwitchingViewModel(string tag) : base(tag)
        {
        }

        protected virtual void Start()
        {
            _viewsSwitchingMediator = new ViewsSwitchingMediator(viewsSwitchingController, viewsSwitchingAnimationBinding, View.ViewName);
        }

        public void SwitchToView(string viewName, FormsTransitionBundle formsTransitionBundle, bool recreateViewToSwitchTo = false)
        {
            _viewsSwitchingMediator.SwitchToView(viewName, formsTransitionBundle, recreateViewToSwitchTo);
        }

        public void SwitchToView(string viewName, bool recreateViewToSwitchTo)
        {
            _viewsSwitchingMediator.SwitchToView(viewName, recreateViewToSwitchTo);
        }

        public void SwitchToView(string viewName)
        {
            _viewsSwitchingMediator.SwitchToView(viewName);
        }

        public void SwitchToPreviousView(in FormsTransitionBundle formsTransitionBundle, bool recreateViewToSwitchTo)
        {
            _viewsSwitchingMediator.SwitchToPreviousView(in formsTransitionBundle, recreateViewToSwitchTo);
        }

        public void SwitchToView(ViewsPairInfo viewsPairInfo, FormsTransitionBundle formsTransitionBundle = default, bool recreateViewToSwitchTo = false)
        {
            _viewsSwitchingMediator.SwitchToView(viewsPairInfo, formsTransitionBundle, recreateViewToSwitchTo);
        }

        public void SwitchToPreviousView()
        {
            _viewsSwitchingMediator.SwitchToPreviousView();
        }

        public void SwitchToView(ViewsPairInfo viewsPairInfo, in ViewsSwitchingParameters defaultViewsSwitchingParameters,
            bool recreateViewToSwitchTo = false, FormsTransitionBundle formsTransitionBundle = default)
        {
            _viewsSwitchingMediator.SwitchToView(viewsPairInfo, in defaultViewsSwitchingParameters, recreateViewToSwitchTo, formsTransitionBundle);
        }
    }
}