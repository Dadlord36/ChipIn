using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;
using UnityWeld.Binding;


namespace Views
{
    [Binding]
    public class LoginView : MonoBehaviour
    {
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private Button confirmationButton;

        private void Awake()
        {
            Assert.IsNotNull(emailInputField);
            Assert.IsNotNull(passwordInputField);

            emailInputField.contentType = TMP_InputField.ContentType.EmailAddress;
            passwordInputField.contentType = TMP_InputField.ContentType.Password;
        }
        [Binding]
        public void SwitchButtonInteractivity()
        {
            confirmationButton.interactable = !confirmationButton.interactable;
        }
    }
}