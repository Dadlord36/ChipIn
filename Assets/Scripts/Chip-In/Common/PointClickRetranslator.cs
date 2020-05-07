using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common
{
    public sealed class PointClickRetranslator : UIBehaviour, IPointerClickHandler
    {
        public UnityEvent pointerClicked;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClicked();
        }

        private void OnPointerClicked()
        {
            pointerClicked?.Invoke();
        }
    }
}