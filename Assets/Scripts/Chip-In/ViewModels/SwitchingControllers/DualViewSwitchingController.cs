using ScriptableObjects.SwitchBindings;
using UnityEngine;

namespace ViewModels.SwitchingControllers
{
    [CreateAssetMenu(fileName = nameof(DualViewSwitchingController),
        menuName = nameof(SwitchingControllers) + "/" + nameof(DualViewSwitchingController), order = 0)]
    public sealed class DualViewSwitchingController : BaseViewSwitchingController
    {

        protected override void ProcessViewsSwitching(in string viewNameToSwitchTo, ViewsSwitchData.AppearingSide viewAppearingSide)
        {
            viewsSwitchingBindingObject.SwitchViews(viewNameToSwitchTo, viewAppearingSide);
        }
    }
}