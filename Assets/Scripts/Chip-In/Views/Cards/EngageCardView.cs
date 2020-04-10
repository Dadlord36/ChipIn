using System;
using UnityEngine.EventSystems;


namespace Views.Cards
{
    public sealed class EngageCardView : BaseView, IPointerClickHandler
    {
        public event Action WasClicked;
        public void OnPointerClick(PointerEventData eventData)
        {
            OnWasClicked();
        }

        private void OnWasClicked()
        {
            WasClicked?.Invoke();
        }
    }
}