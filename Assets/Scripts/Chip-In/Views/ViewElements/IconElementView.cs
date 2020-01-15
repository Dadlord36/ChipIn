using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views.ViewElements
{
    public class IconElementView : UIBehaviour
    {
        [SerializeField] private Image iconImageUi;

        public Sprite Icon
        {
            get => iconImageUi.sprite;
            set => iconImageUi.sprite = value;
        }
    }
}