using UnityEngine;
using UnityEngine.UI;

namespace ViewModels.UI.Elements.Icons
{
    public class GameSlotIconView : BaseIconView
    {
        private bool _activityState;
        private static readonly Color InactiveColor = Color.gray;
        private static readonly Color ActiveColor = Color.white;

        public Sprite SlotIcon
        {
            get => IconSprite;
            set => IconSprite = value;
        }

        public bool ActivityState
        {
            get => _activityState;
            set
            {
                _activityState = value;
                UpdateActivityStateRepresentation();
            }
        }

        private void UpdateActivityStateRepresentation()
        {
            if (ActivityState)
                MakeActive();
            else
            {
                MakeInactive();
            }
        }

        private void MakeInactive()
        {
           SetIconColor(InactiveColor);
        }

        private void MakeActive()
        {
            SetIconColor(ActiveColor);
        }
    }
}