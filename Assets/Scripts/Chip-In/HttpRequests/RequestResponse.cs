using System.Net.Http;

namespace HttpRequests
{
    public struct RequestResponse<TDataModel>
    {
        public readonly HttpResponseMessage responseMessage;
        public readonly TDataModel responseData;

        public RequestResponse(HttpResponseMessage responseMessage, TDataModel responseData)
        {
            this.responseMessage = responseMessage;
            this.responseData = responseData;
        }

    }
}