using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common
{
    public sealed class PointClickRetranslator : UIBehaviour, IPointerClickHandler
    {
        public UnityEvent pointerClicked;

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnPointerClicked();
        }

        private void OnPointerClicked()
        {
            pointerClicked?.Invoke();
        }
    }
}