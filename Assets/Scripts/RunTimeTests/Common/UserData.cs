using DataModels;

namespace RunTimeTests.Common
{
    public static class UserData
    {
        public const string CorrectEmail = "test@mail.com", CorrectPassword = "12345678";
        public const string WrongEmail = "testWrongMail", WrongPassword = "3216";

        public static UserLoginModel correctUserLoginDataModel =
            new UserLoginModel {Email = CorrectEmail, Password = CorrectPassword};

        public static UserLoginModel wrongPasswordUserLoginDataModel =
            new UserLoginModel {Email = CorrectEmail, Password = WrongPassword};

        public static UserLoginModel wrongEmailUserLoginDataModel =
            new UserLoginModel {Email = WrongEmail, Password = CorrectPassword};

        public static UserSimpleRegistrationModel correctUserSimpleRegistrationModel = new UserSimpleRegistrationModel
        {
            Email = CorrectEmail, Password = CorrectPassword
        };
        
        public static UserSimpleRegistrationModel wrongEmailSimpleRegistrationModel = new UserSimpleRegistrationModel
        {
            Email = WrongEmail, Password = CorrectPassword
        };
        
        public static UserSimpleRegistrationModel wrongPasswordUserSimpleRegistrationModel = new UserSimpleRegistrationModel
        {
            Email = CorrectEmail, Password = WrongPassword
        };
    }
}