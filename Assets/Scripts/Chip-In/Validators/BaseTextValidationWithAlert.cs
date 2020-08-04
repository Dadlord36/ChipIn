using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using ScriptableObjects.Validations;
using UnityEngine;
using UnityWeld.Binding;

namespace Validators
{
    public interface IValidationWithAlert
    {
        void ShowAlertIfIsNotValid();
        bool IsValid { get; }
    }


    [Binding]
    public abstract class BaseTextValidationWithAlert<T> : BaseValidationWithAlert
    {
        [SerializeField] private TextValidation validation;

        [Binding] public T TextToValidate { get; set; }

        private object PropertyToValidate => TextToValidate;


        protected override bool CheckIsValid()
        {
            return IsValid = validation.CheckIsValid(PropertyToValidate);
        }
    }
}