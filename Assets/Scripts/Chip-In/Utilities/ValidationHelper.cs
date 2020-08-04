using UnityEngine;
using Validators;

namespace Utilities
{
    public static class ValidationHelper
    {
        public static bool CheckIfAllFieldsAreValid(Component owner)
        {
            var result = owner.GetComponentsInChildren<IValidationWithAlert>();

            foreach (var validationWithAlert in result)
            {
                validationWithAlert.ShowAlertIfIsNotValid();
            }

            foreach (var validationWithAlert in result)
            {
                if (validationWithAlert.IsValid is false)
                    return false;
            }

            return true;
        }
    }
}