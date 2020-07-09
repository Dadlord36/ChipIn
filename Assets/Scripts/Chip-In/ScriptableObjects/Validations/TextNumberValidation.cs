using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(TextNumberValidation), menuName = nameof(Validations) + "/" + nameof(TextNumberValidation),
        order = 0)]
    public sealed class TextNumberValidation : TextValidation
    {
        [Tooltip("Minimal number inclusively")]
        [SerializeField] private int minNumber;
        [Tooltip("Maximal number inclusively")]
        [SerializeField] private int maxNumber;
        public override bool CheckIsValid(object dataToValidate)
        {
            var number = int.Parse((string)dataToValidate) ;
            return number >= minNumber && number <= maxNumber;
        }
    }
}