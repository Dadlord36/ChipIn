using UnityEngine;

namespace ViewModels.UI.Elements.Icons
{
    public class GameSlotIconView : BaseIconView
    {
        public Sprite SlotIcon
        {
            get => IconSprite;
            set => IconSprite = value;
        }
    }
}