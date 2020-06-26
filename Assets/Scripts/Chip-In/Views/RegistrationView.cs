using TMPro;
using UnityEngine;
using Utilities;

namespace Views
{
    public sealed class RegistrationView : BaseView
    {
        [SerializeField] private TMP_InputField emailInputField;
        [SerializeField] private TMP_InputField passwordInputField;
        [SerializeField] private TMP_InputField repeatPasswordInputField;

        public RegistrationView() : base(nameof(RegistrationView))
        {
        }

        protected override void Start()
        {
            base.Start();
            InputFieldsUtility.BindInputFieldSelectionOnSubmit(emailInputField, passwordInputField);
            InputFieldsUtility.BindInputFieldSelectionOnSubmit(passwordInputField, repeatPasswordInputField);
            InputFieldsUtility.BindInputFieldClearingOnSelection(passwordInputField);
            InputFieldsUtility.BindInputFieldClearingOnSelection(repeatPasswordInputField);
        }

        protected override void OnBeingSwitchedTo()
        {
            base.OnBeingSwitchedTo();
            ClearFields();
        }

        private void ClearFields()
        {
            InputFieldsUtility.ClearInputField(emailInputField);
            InputFieldsUtility.ClearInputField(passwordInputField);
            InputFieldsUtility.ClearInputField(repeatPasswordInputField);
        }
    }
}