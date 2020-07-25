using ScriptableObjects.SwitchBindings;
using UnityEngine;

namespace ViewModels.SwitchingControllers
{
    [CreateAssetMenu(fileName = nameof(DualViewSwitchingController),
        menuName = nameof(SwitchingControllers) + "/" + nameof(DualViewSwitchingController), order = 0)]
    public sealed class DualViewSwitchingController : BaseViewSwitchingController
    {
        protected override void ProcessViewsSwitching(in string fromViewName, in string toViewName, 
            FormsTransitionBundle formsTransitionBundle)
        {
            viewsSwitchingBindingObject.SwitchViews(new ViewsPairInfo(fromViewName, toViewName),formsTransitionBundle);
        }
    }
}