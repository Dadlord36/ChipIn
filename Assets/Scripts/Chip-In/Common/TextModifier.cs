using Game.TwitchSettingsMenu.Common;
using UnityEngine.UI;

namespace Common
{
    public sealed class TextModifier
    {
        private readonly string _textToAdd;
        private readonly InputField _controlledInputField;

        public TextModifier(string textToAdd, InputField controlledInputField)
        {
            _textToAdd = textToAdd;
            _controlledInputField = controlledInputField;
            Initialize();
        }

        private string TextInTextField
        {
            get => _controlledInputField.text;
            set
            {
                SwitchTextFieldToStandard();
                var charactersLimit = _controlledInputField.characterLimit;
                _controlledInputField.characterLimit = 0;
                _controlledInputField.text = value;
                _controlledInputField.characterLimit = charactersLimit;
                SwitchTextFieldToIntegers();
            }
        }

        private void Initialize()
        {
            RemodifyText();
        }

        public void RemoveExtraText()
        {
            TextInTextField = StringUtility.GetIntPartOfString(TextInTextField).ToString();
        }

        private void ModifyText()
        {
            TextInTextField = $"{TextInTextField}{_textToAdd}";
        }

        public void RemodifyText()
        {
            RemoveExtraText();
            ModifyText();
        }

        private void SwitchTextFieldToStandard()
        {
            _controlledInputField.contentType = InputField.ContentType.Standard;
        }

        private void SwitchTextFieldToIntegers()
        {
            _controlledInputField.contentType = InputField.ContentType.IntegerNumber;
        }
    }
}