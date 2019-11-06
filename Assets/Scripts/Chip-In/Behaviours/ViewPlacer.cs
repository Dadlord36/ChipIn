using ScriptableObjects;
using UnityEngine;

namespace Behaviours
{
    public abstract class ViewPlacer : MonoBehaviour
    {
        [SerializeField] private ViewsSwitchingBinding viewsSwitchingBinding;
        [SerializeField] private ViewsRetrievingBinding viewsRetrievingBinding;

        private void OnEnable()
        {
            viewsSwitchingBinding.ViewSwitchingRequested += ReplaceCurrentViewsWithGiven;
        }

        private void OnDisable()
        {
            viewsSwitchingBinding.ViewSwitchingRequested -= ReplaceCurrentViewsWithGiven;
        }

        protected void ReleaseSingleSlot(ViewSlot slot)
        {
            if (slot.Occupied)
            {
                viewsRetrievingBinding.RetrieveView(slot.DetachView());
            }
        }

        protected abstract void ReplaceCurrentViewsWithGiven(ViewsSwitchingBinding.ViewsSwitchData viewsSwitchData);
    }
}