using System;
using System.Threading.Tasks;
using DataModels;
using HttpRequests;
using HumbleObjects;
using NUnit.Framework;
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
            var successful = Task.Run(async () => await TryToLogin(UserData.correctUserLoginDataModel)).GetAwaiter()
                .GetResult();

            Assert.IsTrue(successful);
        }

        [Test]
        public void LoginWrongEmailTest()
        {
            var successful = Task.Run(async () => await TryToLogin(UserData.wrongEmailUserLoginDataModel)).GetAwaiter()
                .GetResult();

            Assert.False(successful);
        }

        private static async Task<bool> TryToLogin(UserLoginModel loginModel)
        {
            ApiHelper.InitializeClient();
            
            bool successful = false;
            try
            {
                successful = await LoginProcessor.Login(loginModel);
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