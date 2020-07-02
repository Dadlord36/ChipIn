using ScriptableObjects.SwitchBindings;
using UnityEngine;
using Utilities;

namespace Views.ViewElements.ViewsPlacers
{
    public abstract class MultiViewsPlacer : MonoBehaviour
    {
        [SerializeField] private ViewsSwitchingBinding viewsSwitchingBinding;
        [SerializeField] private ViewsRetrievingBinding viewsRetrievingBinding;

        protected void ReleaseSingleSlot(ViewSlot slot)
        {
            if (slot.Occupied)
            {
                viewsRetrievingBinding.RetrieveView(slot.DetachView());
            }
            else
            {
                LogUtility.PrintLogError(GetType().Name, "View slot was empty and can't be released", this);
            }
        }
        private void OnEnable()
        {
            viewsSwitchingBinding.ViewSwitchingRequested += ReplaceCurrentViewWithGiven;
        }

        private void OnDisable()
        {
            viewsSwitchingBinding.ViewSwitchingRequested -= ReplaceCurrentViewWithGiven;
        }

        protected abstract void ReplaceCurrentViewWithGiven(BaseView viewToSwitchTo);
    }
}