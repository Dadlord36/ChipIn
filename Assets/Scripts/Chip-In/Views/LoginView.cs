using TMPro;
using UnityEngine;
using Utilities;

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
            InputFieldsUtility.BindInputFieldSelectionOnSubmit(emailField,passwordField);
            InputFieldsUtility.BindInputFieldClearingOnSelection(passwordField);
        }

        protected override void OnBeingSwitchedTo()
        {
            base.OnBeingSwitchedTo();
            ClearFields();
        }
        
        private void ClearFields()
        {
            InputFieldsUtility.ClearInputField(emailField);
            InputFieldsUtility.ClearInputField(passwordField);
        }
    }
}