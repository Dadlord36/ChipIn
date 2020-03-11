using System.Collections.Generic;
using System.Net.Http;
using DataModels.HttpRequestsHeadersModels;
using DataModels.RequestsModels;
using GlobalVariables;
using Newtonsoft.Json;
using Repositories.Interfaces;

namespace HttpRequests.RequestsProcessors.DeleteRequests
{
    public interface IErrorsList
    {
        [JsonProperty("errors")] IList<string> Errors { get; set; }
    }

    public class SignOutRequestBodyModel : IBaseDeviceData
    {
        public string DeviceId { get; set; }
        public string Platform { get; set; }
    }

    public interface ISignOutResponseModel : ISuccess, IErrorsList
    {
    }

    public class SignOutResponseModel : ISignOutResponseModel
    {
        public bool Success { get; set; }
        public IList<string> Errors { get; set; }
    }

    public class SignOutRequestProcessor : BaseRequestProcessor<IBaseDeviceData, SignOutResponseModel, ISignOutResponseModel>
    {
        public SignOutRequestProcessor(IRequestHeaders requestHeaders,
            IBaseDeviceData requestBodyModel) :
            base(ApiCategories.SingOut, HttpMethod.Delete, requestHeaders, requestBodyModel)
        {
        }
    }
}