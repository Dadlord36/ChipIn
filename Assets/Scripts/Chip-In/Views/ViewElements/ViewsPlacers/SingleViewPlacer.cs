using ScriptableObjects.SwitchBindings;
using UnityEngine;

namespace Views.ViewElements.ViewsPlacers
{
    public abstract class SingleViewPlacer : BaseViewsPlacer
    {
        [SerializeField] private SingleViewSwitchingBinding singleViewSwitchingBinding;

        private void OnEnable()
        {
            singleViewSwitchingBinding.ViewSwitchingRequested += ReplaceCurrentViewWithGiven;
        }

        private void OnDisable()
        {
            singleViewSwitchingBinding.ViewSwitchingRequested -= ReplaceCurrentViewWithGiven;
        }

        protected abstract void ReplaceCurrentViewWithGiven(BaseView givenView);
    }
}