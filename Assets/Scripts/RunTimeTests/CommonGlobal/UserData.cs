using DataModels;

namespace RunTimeTests.CommonGlobal
{
    public static class UserData
    {
        public const string CorrectEmail = "test@mail.com", CorrectPassword = "12345678";
        public const string WrongEmail = "testWrong@mail.com", WrongPassword = "32165487";

        public static UserLoginModel correctUserLoginDataModel =
            new UserLoginModel {Email = CorrectEmail, Password = CorrectPassword};

        public static UserLoginModel wrongPasswordUserLoginDataModel =
            new UserLoginModel {Email = CorrectEmail, Password = WrongPassword};

        public static UserLoginModel wrongEmailUserLoginDataModel =
            new UserLoginModel {Email = WrongEmail, Password = CorrectPassword};

        public static UserSimpleRegistrationModel correctUserSimpleRegistrationModel = new UserSimpleRegistrationModel
        {
            Email = CorrectPassword, Password = CorrectPassword
        };
        
        public static UserSimpleRegistrationModel wrongEmailSimpleRegistrationModel = new UserSimpleRegistrationModel
        {
            Email = WrongEmail, Password = CorrectPassword
        };
        
        public static UserSimpleRegistrationModel wrongPasswordUserSimpleRegistrationModel = new UserSimpleRegistrationModel
        {
            Email = CorrectPassword, Password = WrongPassword
        };
    }
}