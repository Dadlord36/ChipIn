using TMPro;
using UnityEngine;

namespace Views
{
    public class LoginView : BaseView
    {
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField passwordField;

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
    }
}