using System;
using System.Threading.Tasks;
using DataModels.HttpRequestsHeadersModels;
using DataModels.RequestsModels;
using DataModels.ResponsesModels;
using HttpRequests.RequestsProcessors;
using HttpRequests.RequestsProcessors.DeleteRequests;
using HttpRequests.RequestsProcessors.PostRequests;
using Newtonsoft.Json;
using UnityEngine;
using Utilities;

namespace RequestsStaticProcessors
{
    public static class SessionStaticProcessor
    {
        private const string Tag = nameof(SessionStaticProcessor);

        public static async Task<BaseRequestProcessor<IUserLoginRequestModel, LoginResponseModel, ILoginResponseModel>.
                HttpResponse>
            Login(IUserLoginRequestModel userLoginRequestModel)
        {
            try
            {
                Debug.Log($"Login request model: {JsonConvert.SerializeObject(userLoginRequestModel)}");
                return await new LoginRequestProcessor(userLoginRequestModel).SendRequest("User was LoggedIn");
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                
            }

            return default;
        }

        public static async Task LogOut(IRequestHeaders requestHeaders, IBaseDeviceData deviceData)
        {
            var response = await new SignOutRequestProcessor(requestHeaders, deviceData).SendRequest("User");
            LogUtility.PrintLog(Tag, $"SignOut success message: {response.ResponseModelInterface.Success.ToString() }");
            if (!response.ResponseModelInterface.Success)
            {
                LogUtility.PrintLog(Tag, $"Error message: {response.ResponseModelInterface.Errors[0]}");
            }
        }
    }
}