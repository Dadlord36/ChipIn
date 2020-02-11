using TMPro;
using UnityEngine;

namespace Views
{
    public class LoginView : BaseView
    {
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField passwordField;

        protected override void OnEnable()
        {
            base.OnEnable();
            ClearFields();
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                SwitchSelectedInputField();
            }
        }

        private void SwitchSelectedInputField()
        {
            if (emailField.isFocused)
            {
                passwordField.Select();
                return;
            }
            emailField.Select();
        }

        private void ClearFields()
        {
            ClearInputField(emailField);
            ClearInputField(passwordField);
        }

        private static void ClearInputField(TMP_InputField inputField)
        {
            inputField.text = "";
            inputField.caretPosition = 0;
        }
    }
}