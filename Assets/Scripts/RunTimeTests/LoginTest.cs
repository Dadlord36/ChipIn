using System;
using System.Threading.Tasks;
using DataModels;
using HttpRequests;
using HumbleObjects;
using NUnit.Framework;
using RunTimeTests.CommonGlobal;
using UnityEngine;
using Utilities.ApiExceptions;

namespace RunTimeTests
{

    public class LoginTest
    {
        [Test]
        public void LoginSuccessfulTest()
        {
            bool successful = Task.Run(async () => await TryToLogin(UserData.correctUserLoginDataModel)).GetAwaiter()
                .GetResult();

            Assert.IsTrue(successful);
        }

        [Test]
        public void LoginWrongEmailTest()
        {
            bool successful = Task.Run(async () => await TryToLogin(UserData.wrongEmailUserLoginDataModel)).GetAwaiter()
                .GetResult();

            Assert.False(successful);
        }

        async Task<bool> TryToLogin(UserLoginModel loginModel)
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