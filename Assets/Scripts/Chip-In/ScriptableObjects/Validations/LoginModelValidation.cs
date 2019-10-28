using DataModels;
using EmailValidation;
using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(LoginModelValidation),
        menuName = "Validations/" + nameof(LoginModelValidation), order = 0)]
    public class LoginModelValidation : DataModelValidation<UserLoginModel>
    {
        public override bool CheckIsValid(UserLoginModel dataModel)
        {
            return CheckEmailIsValid(dataModel.Email) && CheckIfPasswordIsValid(dataModel.Password);
        }

        private static bool CheckIfPasswordIsValid(string dataModelPassword)
        {
            return !string.IsNullOrEmpty(dataModelPassword);
        }

        private static bool CheckEmailIsValid(in string email)
        {
            if (string.IsNullOrEmpty(email)) return false;
            if (EmailValidator.Validate(email))
            {
                Debug.Log("Email is correct");
                return true;
            }

            Debug.Log("Email is incorrect");

            return false;
        }
    }
}