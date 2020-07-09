using DataModels.RequestsModels;
using EmailValidation;
using UnityEngine;
using Utilities;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(LoginModelValidation),
        menuName = "Validations/" + nameof(LoginModelValidation), order = 0)]
    public class LoginModelValidation : DataModelValidation<UserLoginRequestModel>
    {
        private const string Tag = nameof(LoginModelValidation);
        public override bool CheckIsValid(UserLoginRequestModel dataRequestModel)
        {
            return CheckEmailIsValid(dataRequestModel.Email) && CheckIfPasswordIsValid(dataRequestModel.Password);
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
                LogUtility.PrintLog(Tag,"Email is correct");
                return true;
            }

            LogUtility.PrintLog(Tag,"Email is incorrect");

            return false;
        }
    }
}