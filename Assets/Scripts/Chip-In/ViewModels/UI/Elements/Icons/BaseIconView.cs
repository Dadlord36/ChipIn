using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ViewModels.UI.Elements.Icons
{
    public abstract class BaseIconView : UIBehaviour
    {
        [SerializeField] private Image background, iconImage;

        protected Sprite IconSprite
        {
            get => iconImage.sprite;
            set => iconImage.sprite = value;
        }

        protected Sprite Background
        {
            get => background.sprite;
            set => background.sprite = value;
        }
    }
}