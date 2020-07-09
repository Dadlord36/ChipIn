using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Views.InputFields
{
    public class TextCharactersCount : UIBehaviour
    {
        [SerializeField] private TMP_InputField referencedInputField;
        [SerializeField] private TMP_Text currentNumberTextField;
        [SerializeField] private TMP_Text maxNumberTextField;

        private int MaxNumber
        {
            get => int.Parse(maxNumberTextField.text);
            set => maxNumberTextField.text = value.ToString();
        }
        
        private int CurrentNumber
        {
            get => int.Parse(currentNumberTextField.text);
            set => currentNumberTextField.text = value.ToString();
        }
        
        protected override void OnEnable()
        {
            base.OnEnable();
            referencedInputField.onValueChanged.AddListener(UpdateValueReflection);
            SetupMaxNumber();
            UpdateValueReflection(referencedInputField.text);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            referencedInputField.onValueChanged.RemoveListener(UpdateValueReflection);
        }

        private void UpdateValueReflection(string inputFieldText)
        {
            CurrentNumber = inputFieldText.Length;
        }

        private void SetupMaxNumber()
        {
            MaxNumber = referencedInputField.characterLimit;
        }
    }
}