using System.Threading.Tasks;
using DataModels;
using DataModels.RequestsModels;
using HttpRequests;
using NUnit.Framework;
using RequestsStaticProcessors;
using RunTimeTests.Common;
using UnityEngine;
using Utilities.ApiExceptions;

namespace RunTimeTests
{
    public class LoginTest
    {
        [Test]
        public void LoginSuccessfulTest()
        {
            var successful = Task.Run(async () => await TryToLogin(UserData.CorrectUserLoginRequestDataModel)).GetAwaiter()
                .GetResult();

            Assert.IsTrue(successful);
        }

        [Test]
        public void LoginWrongEmailTest()
        {
            var successful = Task.Run(async () => await TryToLogin(UserData.WrongEmailUserLoginRequestDataModel)).GetAwaiter()
                .GetResult();

            Assert.False(successful);
        }

        private static async Task<bool> TryToLogin(IUserLoginRequestModel loginRequestModel)
        {
            ApiHelper.InitializeClient();

            bool successful = false;
            try
            {
                var result = await LoginStaticProcessor.Login(loginRequestModel);
                successful = result.ResponseModelInterface!=null;
            }
            catch (ApiException e)
            {
                Debug.Log(e);
            }

            ApiHelper.Dispose();

            return successful;
        }
    }
}