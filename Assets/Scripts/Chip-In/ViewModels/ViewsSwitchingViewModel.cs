using Factories;
using UnityWeld.Binding;
using ViewModels.Helpers;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel
    {
        private IViewsSwitchingHelper _viewsSwitchingHelper;

        protected virtual void Start()
        {
            _viewsSwitchingHelper = Factory.CreateViewSwitchingHelper();
        }

        protected void SwitchToView(in string viewName)
        {
            _viewsSwitchingHelper.SwitchToView(viewName);
        }
        
        [Binding]
        public void SwitchToPreviousView()
        {
            
        }
    }
}