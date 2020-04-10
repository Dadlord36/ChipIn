using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.InputFields
{
    public class TextCharactersCount : UIBehaviour
    {
        [SerializeField] private TMP_InputField referencedInputField;
        [SerializeField] private TMP_Text textField;

        protected override void OnEnable()
        {
            base.OnEnable();
            referencedInputField.onValueChanged.AddListener(UpdateValueReflection);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            referencedInputField.onValueChanged.RemoveListener(UpdateValueReflection);
        }

        private void UpdateValueReflection(string inputFieldText)
        {
            textField.text = inputFieldText.Length.ToString();
        }
    }
}