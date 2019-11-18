using ScriptableObjects;
using ScriptableObjects.SwitchBindings;
using UnityEngine;

namespace Views.ViewElements
{
    public abstract class ViewPlacer : MonoBehaviour
    {
        [SerializeField] private MultiViewsSwitchingBinding multiViewsSwitchingBinding;
        [SerializeField] private ViewsRetrievingBinding viewsRetrievingBinding;

        private void OnEnable()
        {
            multiViewsSwitchingBinding.ViewSwitchingRequested += ReplaceCurrentMultiViewsWithGiven;
        }

        private void OnDisable()
        {
            multiViewsSwitchingBinding.ViewSwitchingRequested -= ReplaceCurrentMultiViewsWithGiven;
        }

        protected void ReleaseSingleSlot(ViewSlot slot)
        {
            if (slot.Occupied)
            {
                viewsRetrievingBinding.RetrieveView(slot.DetachView());
            }
        }

        protected abstract void ReplaceCurrentMultiViewsWithGiven(MultiViewsSwitchingBinding.ViewsSwitchData viewsSwitchData);
    }
}