using ScriptableObjects.Interfaces;
using UnityEngine.Assertions;

namespace ViewModels.Helpers
{
    public sealed class SingleViewSwitchingHelper : BaseViewSwitchingHelper
    {
        private ISingleViewSwitchingBinding _singleViewSwitchingBinding;
        protected override void Awake()
        {
            base.Awake();
            _singleViewSwitchingBinding = viewsSwitchingBindingObject as ISingleViewSwitchingBinding;
            Assert.IsNotNull(_singleViewSwitchingBinding);
        }

        protected override void ProcessViewsSwitching(in string viewNameToSwitchTo)
        {
            _singleViewSwitchingBinding.SwitchViews(viewNameToSwitchTo);
        }
    }
}