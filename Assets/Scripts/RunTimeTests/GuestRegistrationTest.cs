using System.Threading.Tasks;
using Common;
using NUnit.Framework;
using RunTimeTests.Common;

namespace RunTimeTests
{
    public class GuestRegistrationTest
    {
        [Test]
        public void GuestRegistrationTestSimplePasses()
        {
            bool successful = Task.Run(async () =>
                    await AsyncRegistrationHelper.TryToRegister(PredefinedUserData.GuestDataModel))
                .GetAwaiter().GetResult();

            Assert.IsTrue(successful);
        }
    }
}