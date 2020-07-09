using System;
using Common;
using Common.Interfaces;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Utilities;

namespace ViewModels.UI.Elements.Counters
{
    public sealed class AdjustableInputFieldView : InputField, ISelectableObject
    {
        #region Events

        public event Action Selected;
        public event Action Deselected;
        public event Action<int> ValueChanged;

        #endregion

        [SerializeField] private string textToAddInTheEnd;
        private int _integerValue;

        private TextModifier _textModifier;

        private void InitializeTextModifier()
        {
            if (_textModifier == null)
                _textModifier = new TextModifier(textToAddInTheEnd, this);
        }

        protected override void Start()
        {
            base.Start();
            _textModifier.RemodifyText();
        }

        private void GetIntegerFromEnteredText()
        {
            _integerValue = StringUtility.GetIntPartOfString(text);
        }

        public override void OnSelect(BaseEventData eventData)
        {
            base.OnSelect(eventData);

            _textModifier.RemoveExtraText();
            OnSelected();
        }

        public override void OnDeselect(BaseEventData eventData)
        {
            base.OnDeselect(eventData);
            OnDeselected();
        }

        private Func<int, bool> _checkIfInputIsValid = delegate(int i) { return i >= 0; };

        public int TextAsInt
        {
            get => _integerValue;
            set
            {
                if (!_checkIfInputIsValid(value)) return;
                _integerValue = value;
                text = value.ToString();
            }
        }


        public Func<int, bool> ValidateInput
        {
            set => _checkIfInputIsValid = value;
        }

        public void Add()
        {
            TextAsInt++;
            ConfirmTextChange();
        }

        public void Subtract()
        {
            TextAsInt--;
            ConfirmTextChange();
        }

        private void OnEndEdit(string newText)
        {
            GetIntegerFromEnteredText();
            ConfirmTextChange();
        }

        private void ConfirmTextChange()
        {
            OnValueChanged();
            _textModifier.RemodifyText();
        }

        protected override void OnEnable()
        {
            base.OnEnable();

            GetIntegerFromEnteredText();
            InitializeTextModifier();
            SubscribeOnEvents();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            UnsubscribeFromEvents();
        }

        private void SubscribeOnEvents()
        {
            onEndEdit.AddListener(OnEndEdit);
        }

        private void UnsubscribeFromEvents()
        {
            onEndEdit.RemoveListener(OnEndEdit);
        }


        private void OnValueChanged()
        {
            ValueChanged?.Invoke(_integerValue);
        }


        private void OnSelected()
        {
            Selected?.Invoke();
        }

        private void OnDeselected()
        {
            Deselected?.Invoke();
        }
    }
}