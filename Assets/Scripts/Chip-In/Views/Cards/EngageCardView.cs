using System;

namespace Views.Cards
{
    public sealed class EngageCardView : BaseView
    {
        public event Action WasClicked;

        public EngageCardView() : base(nameof(EngageCardView))
        {
        }

        public void OnPointerClick()
        {
            OnWasClicked();
        }

        private void OnWasClicked()
        {
            WasClicked?.Invoke();
        }
        
    }
}