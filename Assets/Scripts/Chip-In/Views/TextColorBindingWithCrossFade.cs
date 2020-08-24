using ScriptableObjects.Parameters;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views
{
    public class TextColorBindingWithCrossFade : UIBehaviour
    {
        [SerializeField] private TMP_Text textField;
        [SerializeField] private FloatParameter crossFadeSpeed;

        private Color _selectedColor = Color.white;

        private Color TextFieldColorValue
        {
            get => textField.color;
            set => CrossFadeInstantly(value);
        }

        public Color TextColor
        {
            get => textField.color;
            set
            {
                if (value == _selectedColor) return;
                _selectedColor = value;
                CrossFadeInstantly(value);
            }
        }

        public Color InitialTextColor 
        {
            get => TextFieldColorValue;
            set => TextFieldColorValue = value; 
        }

        private void CrossFadeInstantly(in Color newColor)
        {
            textField.CrossFadeColor(newColor, 0f, true, false);
        }

        private void CrossFadeToColor(in Color newColor)
        {
            textField.CrossFadeColor(newColor, crossFadeSpeed.value, true, false);
        }
    }
}