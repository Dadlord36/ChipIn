using EmailValidation;
using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(EmailValidation),
        menuName = "Validations/" + nameof(EmailValidation), order = 0)]
    public class EmailValidation : DataModelValidation<string>
    {
        public override bool CheckIsValid(string email)
        {
            return CheckEmailIsValid(email);
        }
        
        private static bool CheckEmailIsValid(string email)
        {
            return !string.IsNullOrEmpty(email) && EmailValidator.Validate(email);
        }
    }
}