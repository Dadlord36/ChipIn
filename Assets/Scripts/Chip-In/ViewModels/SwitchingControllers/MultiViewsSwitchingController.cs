using ScriptableObjects.Interfaces;
using UnityEngine.Assertions;

namespace ViewModels.SwitchingControllers
{
    public sealed class MultiViewsSwitchingController : BaseViewSwitchingController
    {
        private IMultiViewsSwitchingBinding _multiViewsSwitchingBinding;
        private string _currentViewName;

        protected override void OnEnable()
        {
            base.OnEnable();
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