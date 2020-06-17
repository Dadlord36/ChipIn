using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Views
{
    public sealed class LoginView : BaseView
    {
        [SerializeField] private TMP_InputField emailField;
        [SerializeField] private TMP_InputField passwordField;

        private TouchScreenKeyboard _keyboard;
        private bool _emailFieldIsSelected;

        private TMP_InputField _selectedInputField;

        public LoginView() : base(nameof(LoginView))
        {
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            ClearFields();
            emailField.onSelect.AddListener(OpenEmailKeyboard);
            passwordField.onSelect.AddListener(OpenPasswordKeyboard);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            emailField.onSelect.RemoveListener(OpenEmailKeyboard);
            passwordField.onSelect.RemoveListener(OpenPasswordKeyboard);
        }

        private void OpenEmailKeyboard(string text)
        {
            _keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.EmailAddress);
            _emailFieldIsSelected = true;
            _selectedInputField = emailField;
        }

        private void OpenPasswordKeyboard(string text)
        {
            _keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true);
            _selectedInputField = passwordField;
        }


        private void Update()
        {
#if UNITY_EDITOR
            if (Input.GetKeyUp(KeyCode.Tab))
            {
                SwitchSelectedInputField();
            }

#elif UNITY_ANDROID || UNITY_IOS
            ProcessMobileKeyboard();
#endif
        }

        private void ProcessMobileKeyboard()
        {
            if (_keyboard == null) return;

            _selectedInputField.text = _keyboard.text;


            if (_keyboard.status != TouchScreenKeyboard.Status.Done) return;

            _keyboard = null;

            if (!_emailFieldIsSelected) return;
            Invoke(nameof(SwitchToPasswordInput), 0.1f);
        }

        private void SwitchToPasswordInput()
        {
            _emailFieldIsSelected = false;
            SetInputFieldActive(passwordField);
        }

        private void SwitchSelectedInputField()
        {
            if (emailField.isFocused)
            {
                SetInputFieldActive(passwordField);
                return;
            }

            SetInputFieldActive(emailField);
        }

        private static void SetInputFieldActive(TMP_InputField inputField)
        {
            var system = EventSystem.current;

            inputField.OnPointerClick(new PointerEventData(system));
            system.SetSelectedGameObject(inputField.gameObject, new BaseEventData(system));
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