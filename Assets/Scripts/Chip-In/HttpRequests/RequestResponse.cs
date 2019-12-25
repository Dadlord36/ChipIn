using System.Net.Http;

namespace HttpRequests
{
    public struct RequestResponse<TDataModel>
    {
        public readonly HttpResponseMessage ResponseMessage;
        public readonly TDataModel ResponseData;

        public RequestResponse(HttpResponseMessage responseMessage, TDataModel responseData)
        {
            ResponseMessage = responseMessage;
            ResponseData = responseData;
        }

    }
}