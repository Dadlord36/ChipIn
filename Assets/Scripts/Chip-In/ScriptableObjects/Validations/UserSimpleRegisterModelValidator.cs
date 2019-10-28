using DataModels;
using EmailValidation;
using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(UserSimpleRegisterModelValidator),
        menuName = "Validations/" + nameof(UserSimpleRegisterModelValidator), order = 0)]
    public class UserSimpleRegisterModelValidator : DataModelValidation<UserSimpleRegistrationModel>
    {
        public override bool CheckIsValid(UserSimpleRegistrationModel dataModel)
        {
            return CheckPasswordIsValid(dataModel.Password) && CheckEmailIsValid(dataModel.Password);
        }

        private static bool CheckEmailIsValid(string email)
        {
            return !string.IsNullOrEmpty(email) && EmailValidator.Validate(email);
        }

        private static bool CheckPasswordIsValid(string password)
        {
            return !string.IsNullOrEmpty(password) ;
        }
        
        
    }
}