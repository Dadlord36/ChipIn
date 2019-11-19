using ScriptableObjects.SwitchBindings;
using UnityEngine;

namespace Views.ViewElements.ViewsPlacers
{
    public abstract class BaseViewsPlacer : MonoBehaviour
    {
        [SerializeField] private ViewsRetrievingBinding viewsRetrievingBinding;

        protected void ReleaseSingleSlot(ViewSlot slot)
        {
            if (slot.Occupied)
            {
                viewsRetrievingBinding.RetrieveView(slot.DetachView());
            }
        }
    }
}