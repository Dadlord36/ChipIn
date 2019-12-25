using DataModels.RequestsModels;
using EmailValidation;
using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(UserSimpleRegisterModelValidator),
        menuName = "Validations/" + nameof(UserSimpleRegisterModelValidator), order = 0)]
    public class UserSimpleRegisterModelValidator : DataModelValidation<SimpleRegistrationRequestModel>
    {
        [SerializeField] private byte minNumberOfPasswordCharacters;
        public override bool CheckIsValid(SimpleRegistrationRequestModel dataRequestModel)
        {
            return CheckPasswordIsValid(dataRequestModel.Password) && CheckEmailIsValid(dataRequestModel.Email);
        }

        private static bool CheckEmailIsValid(string email)
        {
            return !string.IsNullOrEmpty(email) && EmailValidator.Validate(email);
        }

        private  bool CheckPasswordIsValid(string password)
        {
            return !string.IsNullOrEmpty(password)  && password.Length >= minNumberOfPasswordCharacters;
        }
        
        
    }
}