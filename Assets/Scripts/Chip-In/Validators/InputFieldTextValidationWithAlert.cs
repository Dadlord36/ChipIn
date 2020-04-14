using TMPro;
using UnityEngine;
using UnityWeld.Binding;

namespace Validators
{
    [Binding]
    public sealed class InputFieldTextValidationWithAlert : BaseTextValidationWithAlert
    {
        [SerializeField] private TMP_InputField inputField;


        private void OnEnable()
        {
            inputField.onValueChanged.AddListener(CheckIsValid);
        }

        private void OnDisable()
        {
            inputField.onValueChanged.RemoveListener(CheckIsValid);
        }
    }
}