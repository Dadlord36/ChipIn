using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Views.ViewElements
{
    public class SimpleToggle : Toggle
    {
#if UNITY_EDITOR
        public bool UiElementsReferencesAreValid => labelTextField != null;
#endif

        [SerializeField] private TMP_Text labelTextField;

        public string LabelText
        {
            get => labelTextField.text;
            set => labelTextField.text = value;
        }
    }
}