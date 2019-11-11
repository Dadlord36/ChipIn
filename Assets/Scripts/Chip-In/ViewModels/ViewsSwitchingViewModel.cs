using Factories;
using ViewModels.Helpers;

namespace ViewModels
{
    public abstract class ViewsSwitchingViewModel : BaseViewModel
    {
        private IViewsSwitchingHelper _viewsSwitchingHelper;

        private void Start()
        {
            _viewsSwitchingHelper = Factory.CreateViewSwitchingHelper();
        }

        protected void SwitchToView(in string viewName)
        {
            _viewsSwitchingHelper.SwitchToView(viewName);
        }
    }
}