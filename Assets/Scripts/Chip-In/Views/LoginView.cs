using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views
{
    public sealed class LoginView : BaseView
    {
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField passwordField;
        
        public LoginView() : base(nameof(LoginView))
        {
        }


        protected override void Start()
        {
            base.Start();
            emailField.onSubmit.AddListener(delegate
            {
                ClearInputField(passwordField);
                passwordField.OnSelect(new PointerEventData(EventSystem.current));
            });
            passwordField.onSelect.AddListener(delegate { passwordField.text = string.Empty; });
        }

        protected override void OnBeingSwitchedTo()
        {
            base.OnBeingSwitchedTo();
            ClearFields();
        }
        
        private void ClearFields()
        {
            ClearInputField(emailField);
            ClearInputField(passwordField);
        }

        private static void ClearInputField(TMP_InputField inputField)
        {
            inputField.text = string.Empty;
            inputField.caretPosition = 0;
        }
    }
}