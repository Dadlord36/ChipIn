using System.Threading.Tasks;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.PostRequests;
using Newtonsoft.Json;
using UnityEngine;

namespace RequestsStaticProcessors
{
    public static class LoginStaticProcessor
    {
        public static async Task<BaseRequestProcessor<IUserLoginRequestModel, LoginResponseModel, ILoginResponseModel>.HttpResponse>
            Login(IUserLoginRequestModel userLoginRequestModel)
        {
            Debug.Log($"Login request model: {JsonConvert.SerializeObject(userLoginRequestModel)}");
            return await new LoginRequestProcessor(userLoginRequestModel).SendRequest("User was LoggedIn");
        }
    }
}