using System;
using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(InputFieldNowDateValidation), menuName = nameof(Validations) + "/" + nameof(InputFieldNowDateValidation),
        order = 0)]
    public sealed class InputFieldNowDateValidation : TextValidation
    {
        public override bool CheckIsValid(object dataToValidate)
        {
            if (!(dataToValidate is DateTime date)) throw new Exception($"Given object is not {nameof(DateTime)}");

            var isBeforeNow = date <= DateTime.Now;
            return !isBeforeNow;
        }
    }
}