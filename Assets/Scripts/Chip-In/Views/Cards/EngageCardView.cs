using System;
using UnityEngine.EventSystems;


namespace Views.Cards
{
    public sealed class EngageCardView : BaseView, IPointerClickHandler
    {
        public event Action WasClicked;

        public EngageCardView() : base(nameof(EngageCardView))
        {
        }

        void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
        {
            OnWasClicked();
        }

        private void OnWasClicked()
        {
            WasClicked?.Invoke();
        }
    }
}