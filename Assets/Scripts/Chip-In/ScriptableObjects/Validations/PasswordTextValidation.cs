using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(PasswordTextValidation), menuName = nameof(Validations) + "/" + nameof(PasswordTextValidation),
        order = 0)]
    public class PasswordTextValidation : TextValidation
    {
        [SerializeField] private PasswordValidation passwordValidation;
        public override bool CheckIsValid(object dataToValidate)
        {
            return passwordValidation.CheckIsValid(dataToValidate as string);
        }
    }
}