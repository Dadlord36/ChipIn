using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace Common
{
    public interface IClickable
    {
        void AddOnClickListener(UnityAction action);
        void RemoveOnClickListener(UnityAction action);
    }

    public sealed class PointClickRetranslator : UIBehaviour, IClickable, IPointerClickHandler
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

        public void AddOnClickListener(UnityAction action)
        {
            pointerClicked.AddListener(action);
        }

        public void RemoveOnClickListener(UnityAction action)
        {
            pointerClicked.RemoveListener(action);
        }
    }
}