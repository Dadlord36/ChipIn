using ScriptableObjects.Interfaces;
using UnityEngine;
using UnityEngine.Assertions;

namespace ViewModels.SwitchingControllers
{
    [CreateAssetMenu(fileName = nameof(SingleViewSwitchingController),
        menuName = nameof(SwitchingControllers) + "/" + nameof(SingleViewSwitchingController), order = 0)]
    public sealed class SingleViewSwitchingController : BaseViewSwitchingController
    {
        protected override void ProcessViewsSwitching(in string viewNameToSwitchTo)
        {
            (viewsSwitchingBindingObject as ISingleViewSwitchingBinding).SwitchViews(viewNameToSwitchTo);
        }
    }
}