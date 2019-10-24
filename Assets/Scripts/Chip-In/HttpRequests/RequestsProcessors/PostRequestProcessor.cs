using System.Threading.Tasks;

namespace HttpRequests
{
    public abstract class PostRequestProcessor<TRequestModel,TResponseModel>
        where TRequestModel : class 
        where TResponseModel : class
    {
        private readonly string _requestSuffix;

        protected PostRequestProcessor(string requestSuffix)
        {
            _requestSuffix = requestSuffix;
        }

        public async Task<TResponseModel> SendRequest(TRequestModel requestModel)
        {
            return await HttpRequestProcessor.Post<TResponseModel>(_requestSuffix, requestModel);
        }
    }
}