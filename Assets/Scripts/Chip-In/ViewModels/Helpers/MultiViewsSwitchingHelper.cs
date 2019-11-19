using ScriptableObjects.Interfaces;
using UnityEngine.Assertions;

namespace ViewModels.Helpers
{
    public sealed class MultiViewsSwitchingHelper : BaseViewSwitchingHelper
    {
        private IMultiViewsSwitchingBinding _multiViewsSwitchingBinding;
        private string _currentViewName;

        protected override void Awake()
        {
            base.Awake();
            _multiViewsSwitchingBinding = viewsSwitchingBindingObject as IMultiViewsSwitchingBinding;
            Assert.IsNotNull(_multiViewsSwitchingBinding);
        }

        protected override void ProcessViewsSwitching(in string viewNameToSwitchTo)
        {
            _multiViewsSwitchingBinding.SwitchViews(_currentViewName, viewNameToSwitchTo);
            _currentViewName = viewNameToSwitchTo;
        }
    }
}