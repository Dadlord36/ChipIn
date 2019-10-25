using System.Threading.Tasks;
using DataModels;
using HttpRequests;
using NUnit.Framework;

namespace Tests
{
    public class LoginToAccountTest
    {
        private readonly UserLoginModel _testUserLoginModel = new UserLoginModel
        {
            Password = "12345678", Email = "Dadlord36@outlook.com"
        };
        
        [Test]
        public async Task LoginToTestAccount()
        {
            ApiHelper.InitializeClient();
            var responseData = await new LoginRequestProcessor().SendRequest(_testUserLoginModel);
            Assert.IsTrue(responseData.ResponseMessage.IsSuccessStatusCode);
        }
        
    }
}