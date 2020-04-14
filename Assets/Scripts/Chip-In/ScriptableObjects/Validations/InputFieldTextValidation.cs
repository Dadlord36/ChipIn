using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(InputFieldTextValidation), menuName = nameof(Validations) + "/" + nameof(InputFieldTextValidation),
        order = 0)]
    public sealed class InputFieldTextValidation : TextValidation
    {
        [Tooltip("Minimal characters number inclusively")]
        [SerializeField] private uint minCharactersNumber;
        [Tooltip("Maximal characters number inclusively")]
        [SerializeField] private uint maxCharactersNumber;
        private bool CheckIsValid(in string dateToValidate)
        {
            var stringLength = dateToValidate.Length;
            return stringLength >= minCharactersNumber && stringLength <= maxCharactersNumber;
        }

        public override bool CheckIsValid(object dataToValidate)
        {
            return CheckIsValid(dataToValidate as string);
        }
    }
}