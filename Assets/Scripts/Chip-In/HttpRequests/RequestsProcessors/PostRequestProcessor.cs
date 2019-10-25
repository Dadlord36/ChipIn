using System;
using System.Threading.Tasks;
using Utilities;

namespace HttpRequests
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
                var responseData = await JsonConverterUtility.ContentJsonTo<TResponseModel>(responseMessage.Content);
                return new RequestResponse<TResponseModel>(responseMessage, responseData);
            }
        }
    }
}