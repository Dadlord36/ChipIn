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
    public class RegistrationTest
    {
        [Test]
        public void SuccessRegisterTest()
        {
            bool successful = Task.Run(async () => await TryToRegister(UserData.correctUserSimpleRegistrationModel))
                .GetAwaiter()
                .GetResult();

            Assert.IsTrue(successful);
        }

        /// <summary>
        /// Email address is not present on server 
        /// </summary>
        [Test]
        public void WrongEmailRegisterTest()
        {
            bool successful = Task.Run(async () => await TryToRegister(UserData.wrongEmailSimpleRegistrationModel))
                .GetAwaiter()
                .GetResult();

            Assert.IsTrue(successful);
        }

        private async Task<bool> TryToRegister(UserSimpleRegistrationModel userRegistrationModel)
        {
            ApiHelper.InitializeClient();

            bool successful = false;
            try
            {
                successful = await RegistrationProcessor.RegisterUserSimple(userRegistrationModel);
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