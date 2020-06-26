using TMPro;
using UnityEngine.EventSystems;

namespace Utilities
{
    public static class InputFieldsUtility
    {
        public static void BindInputFieldSelectionOnSubmit(TMP_InputField firstField, TMP_InputField secondField)
        {
            firstField.onSubmit.AddListener(delegate
            {
                ClearInputField(secondField);
                secondField.OnSelect(new PointerEventData(EventSystem.current));
            });
        }

        public static void BindInputFieldClearingOnSelection(TMP_InputField inputField)
        {
            inputField.onSelect.AddListener(delegate { inputField.text = string.Empty;});
        }
        
        public static void ClearInputField(TMP_InputField inputField)
        {
            inputField.text = string.Empty;
            // inputField.caretPosition = 0;
        }
    }
}