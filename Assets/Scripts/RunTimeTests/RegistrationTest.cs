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
    public class RegistrationTest
    {
        [Test]
        public void SuccessRegisterTest()
        {
            bool successful = Task.Run(async () =>
                    await AsyncRegistrationHelper.TryToRegister(UserData.correctUserSimpleRegistrationModel))
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
            bool successful = Task.Run(async () =>
                    await AsyncRegistrationHelper.TryToRegister(UserData.wrongEmailSimpleRegistrationModel))
                .GetAwaiter()
                .GetResult();

            Assert.IsTrue(successful);
        }
    }
}