using System;
using System.Threading.Tasks;
using DataModels;
using HttpRequests;
using UnityEngine;

namespace HumbleObjects
{
    public static class LoginProcessor
    {
        public static async Task<bool> Login(UserLoginModel userLoginModel)
        {
            try
            {
                var responseData = await new LoginRequestProcessor().SendRequest(userLoginModel);
                if (responseData.responseMessage.IsSuccessStatusCode)
                {
                    Debug.Log($"Response is successful");
                    Debug.Log(responseData.responseData.user.ToString());
                    return true;
                }
                else
                {
                    Debug.Log(responseData.responseMessage.ReasonPhrase);
                    return false;
                }
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
    }
}