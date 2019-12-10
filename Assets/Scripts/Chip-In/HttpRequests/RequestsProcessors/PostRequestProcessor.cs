using System;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Utilities.ApiExceptions;

namespace HttpRequests.RequestsProcessors
{
    public abstract class PostRequestProcessor<TRequestModel, TResponseModel>
        where TRequestModel : class
        where TResponseModel : class
    {
        private readonly string _requestSuffix;

        protected PostRequestProcessor(string requestSuffix)
        {
            _requestSuffix = requestSuffix;
        }

        public async Task<RequestResponse<TResponseModel>> SendRequest(TRequestModel requestModel)
        {
            using (var responseMessage = await HttpRequestProcessor.Post(_requestSuffix, requestModel))
            {
                if (responseMessage == null)
                {
                    throw new Exception("Response message is null");
                }

                if (responseMessage.IsSuccessStatusCode)
                {
                    var responseData =
                        await JsonConverterUtility.ContentAsyncJsonTo<TResponseModel>(responseMessage.Content);
                    return new RequestResponse<TResponseModel>(responseMessage, responseData);
                }

                {
                    var responseAsString = await responseMessage.Content.ReadAsStringAsync();
                    var errorMessageBuilder = new StringBuilder();
                    try
                    {
                        errorMessageBuilder.Append(responseAsString);
                    }
                    catch (ApiException _)
                    {
                    }
                    errorMessageBuilder.Append("\r\n");
                    errorMessageBuilder.Append($"Error Code: {responseMessage.StatusCode}");
                    responseMessage.Dispose();
                    throw new ApiException(errorMessageBuilder.ToString());
                }
            }
        }
    }
}