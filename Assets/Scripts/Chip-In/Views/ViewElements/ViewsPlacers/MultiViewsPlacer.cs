using ScriptableObjects.SwitchBindings;
using UnityEngine;

namespace Views.ViewElements.ViewsPlacers
{
    public abstract class MultiViewsPlacer : BaseViewsPlacer
    {
        [SerializeField] private MultiViewsSwitchingBinding multiViewsSwitchingBinding;

        private void OnEnable()
        {
            multiViewsSwitchingBinding.ViewSwitchingRequested += ReplaceCurrentViewWithGiven;
        }

        private void OnDisable()
        {
            multiViewsSwitchingBinding.ViewSwitchingRequested -= ReplaceCurrentViewWithGiven;
        }

        protected abstract void ReplaceCurrentViewWithGiven(MultiViewsSwitchingBinding.DualViewsSwitchData dualViewsSwitchData);
    }
}