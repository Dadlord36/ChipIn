using System.Threading.Tasks;
using NUnit.Framework;
using RunTimeTests.Common;
using Assert = UnityEngine.Assertions.Assert;

namespace RunTimeTests
{
    public class GuestRegistrationTest
    {
        [Test]
        public void GuestRegistrationTestSimplePasses()
        {
            /*bool successful = Task.Run(async () =>
                    await AsyncRegistrationHelper.TryToRegister(PredefinedUserData.GuestDataRequestModel))
                .GetAwaiter().GetResult();

            Assert.IsTrue(successful);*/
        }
    }
}