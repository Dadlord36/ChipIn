using DataModels;
using EmailValidation;
using UnityEngine;

namespace ScriptableObjects.Validations
{
    public class LoginModelValidation : DataModelValidation<UserLoginModel>
    {
        public override bool CheckIsValid(UserLoginModel dataModel)
        {
            return CheckEmailIsValid(dataModel.Email) && CheckIfPasswordIsValid(dataModel.Password);
        }

        private bool CheckIfPasswordIsValid(string dataModelPassword)
        {
            return !string.IsNullOrEmpty(dataModelPassword);
        }

        private bool CheckEmailIsValid(in string email)
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