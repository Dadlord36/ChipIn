using System;
using Factories;
using UnityWeld.Binding;
using ViewModels.Interfaces;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel
    {
        private IViewsSwitchingHelper _viewsSwitchingHelper;

        protected virtual void Start()
        {
            _viewsSwitchingHelper = Factory.CreateMultiViewSwitchingHelper();
        }

        protected void SwitchToView(in string viewName)
        {
            _viewsSwitchingHelper.RequestSwitchToView(viewName);
        }

        public void SwitchToPreviousView()
        {
            _viewsSwitchingHelper.SwitchToPreviousView();
        }
    }
}