using System;
using System.Threading;
using System.Threading.Tasks;
using DataModels.HttpRequestsHeadersModels;
using Factories;
using RestSharp;

namespace Utilities
{
    public class MultipartRestRequestBuilder
    {
        private readonly struct DeeperItemArray
        {
            private readonly string _itemName;
            private readonly string _arrayParameterName;
            private readonly RestRequest _request;

            public DeeperItemArray(string itemName, in string arrayParameterName, RestRequest request)
            {
                _itemName = itemName;
                _arrayParameterName = arrayParameterName;
                _request = request;
            }

            public void AddArrayParameterAttributeParameter(in string parameterName, int index, in string value)
            {
                _request.AddParameter(CreateAdvertFeatureArrayParameterName(parameterName, index), value);
            }

            private string CreateAdvertFeatureArrayParameterName(in string parameterName, int index)
            {
                /*return $"{_itemName}[{_arrayParameterName}][{index}][{parameterName}]";*/
                return $"{_itemName}[{_arrayParameterName}][][{parameterName}]";
            }
        }

        private readonly RestRequest _request;
        private readonly string _itemName;
        private readonly Func<RestRequest, CancellationToken, Task<IRestResponse>> _sendRequestTask;
        private DeeperItemArray _deeperItemArray;


        public MultipartRestRequestBuilder(IRequestHeaders requestHeaders, Method requestMethodType, in string ApiCategory,
            Func<RestRequest, CancellationToken, Task<IRestResponse>> sendRequestTask, string parameterItemName)
        {
            _request = RequestsFactory.MultipartRestRequest(requestHeaders, requestMethodType, ApiCategory);
            _sendRequestTask = sendRequestTask;
            _itemName = parameterItemName;
        }

        public Task<IRestResponse> ExecuteAsync(CancellationToken cancellationToken)
        {
            return _sendRequestTask.Invoke(_request, cancellationToken);
        }

        public void InitializeArrayParameter(string arrayParameterName)
        {
            _deeperItemArray = new DeeperItemArray(_itemName, arrayParameterName, _request);
        }

        public void AddArrayParameterItemParameter(in string parameterName, int index, in string value)
        {
            _deeperItemArray.AddArrayParameterAttributeParameter(parameterName, index, value);
        }

        public void AddFileParam(string hierarchy, in string value)
        {
            _request.AddFile($"{_itemName}{WrapChild(hierarchy)}", value);
        }

        public void AddItemParam(string hierarchy, in string value)
        {
            _request.AddParameter($"{_itemName}{WrapChild(hierarchy)}", value);
        }

        private static string WrapChild(in string text)
        {
            var extra = string.Empty;
            if (!string.IsNullOrEmpty(text))
            {
                extra = $"[{text}]";
            }

            return extra;
        }
    }
}