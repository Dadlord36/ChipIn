using ScriptableObjects.SwitchBindings;
using Tasking;
using UnityEngine;

namespace ViewModels.SwitchingControllers
{
    [CreateAssetMenu(fileName = nameof(DualViewSwitchingController),
        menuName = nameof(SwitchingControllers) + "/" + nameof(DualViewSwitchingController), order = 0)]
    public sealed class DualViewSwitchingController : BaseViewSwitchingController
    {
        protected override void ProcessViewsSwitching(in string fromViewName, in string toViewName,
            bool recreateViewToSwitchTo, FormsTransitionBundle formsTransitionBundle)
        {
            var toSwitchForm = fromViewName;
            var toName = toViewName;
            TasksFactories.ExecuteOnMainThread(() =>
            {
                viewsSwitchingBindingObject.SwitchViews(new ViewsPairInfo(toSwitchForm, toName),formsTransitionBundle,
                    recreateViewToSwitchTo);
            });
        }
    }
}