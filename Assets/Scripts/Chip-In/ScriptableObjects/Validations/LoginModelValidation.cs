using System;
using System.Net.Mail;
using DataModels;
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
            return true;
            try
            {
                var mail = new MailAddress(email);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                return false;
            }

            return true;
        }
    }
}