using System;
using UnityEngine.EventSystems;

namespace Common
{
    public sealed class PointClickRetranslator : UIBehaviour, IPointerClickHandler
    {
        public event Action PointerClicked;
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnPointerClicked();
        }

        private void OnPointerClicked()
        {
            PointerClicked?.Invoke();
        }
    }
}