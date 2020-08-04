using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(EmailTextValidation), menuName = nameof(Validations) + "/" + nameof(EmailTextValidation),
        order = 0)]
    public class EmailTextValidation : TextValidation
    {
        [SerializeField] private EmailValidation emailValidation;
        public override bool CheckIsValid(object dataToValidate)
        {
            return emailValidation.CheckIsValid(dataToValidate as string);
        }
    }
}