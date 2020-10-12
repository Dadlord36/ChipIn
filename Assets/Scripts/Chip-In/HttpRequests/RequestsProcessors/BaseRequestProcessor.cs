using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;
using Controllers;
using DataModels.HttpRequestsHeadersModels;
using Factories;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Utilities;

namespace HttpRequests.RequestsProcessors
{
    public abstract class BaseRequestProcessor<TRequestBodyModelInterface, TResponseModel, TResponseModelInterface>
        where TResponseModel : class, TResponseModelInterface
        where TRequestBodyModelInterface : class
        where TResponseModelInterface : class
    {
        private readonly DisposableCancellationTokenSource _requestCancellationTokenSource = new DisposableCancellationTokenSource();
        private CancellationToken RequestCancellationToken => _requestCancellationTokenSource.Token;

        protected readonly struct BaseRequestProcessorParameters
        {
            public readonly string RequestSuffix;
            public readonly HttpMethod RequestMethod;
            public readonly IReadOnlyList<string> RequestParameters;
            public readonly NameValueCollection QueryStringParameters;
            public readonly IRequestHeaders RequestHeaders;
            public readonly TRequestBodyModelInterface RequestBodyModel;

            public BaseRequestProcessorParameters(string requestSuffix, HttpMethod requestMethod, IRequestHeaders requestHeaders,
                TRequestBodyModelInterface requestBodyModel, IReadOnlyList<string> requestParameters, NameValueCollection queryStringParameters)
            {
                RequestSuffix = requestSuffix;
                RequestParameters = requestParameters;
                QueryStringParameters = queryStringParameters;
                RequestMethod = requestMethod;
                RequestHeaders = requestHeaders;
                RequestBodyModel = requestBodyModel;
            }

            public BaseRequestProcessorParameters(string requestSuffix, HttpMethod requestMethod, IRequestHeaders requestHeaders,
                TRequestBodyModelInterface requestBodyModel, IReadOnlyList<string> requestParameters)
            {
                RequestSuffix = requestSuffix;
                RequestParameters = requestParameters;
                QueryStringParameters = null;
                RequestMethod = requestMethod;
                RequestHeaders = requestHeaders;
                RequestBodyModel = requestBodyModel;
            }
        }

        private const string Tag = "HTTPRequests";

        private readonly BaseRequestProcessorParameters _requestProcessorParameters;
        protected bool SendBodyAsQueryStringFormat;

        protected BaseRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource, string requestSuffix,
            HttpMethod requestMethod, IRequestHeaders requestHeaders, TRequestBodyModelInterface requestBodyModel)
        {
            _requestProcessorParameters = new BaseRequestProcessorParameters(requestSuffix, requestMethod,
                requestHeaders, requestBodyModel, null, null);
            cancellationTokenSource = _requestCancellationTokenSource;
        }

        protected BaseRequestProcessor(out DisposableCancellationTokenSource cancellationTokenSource,
            BaseRequestProcessorParameters requestProcessorParameters)
        {
            _requestProcessorParameters = requestProcessorParameters;
            cancellationTokenSource = _requestCancellationTokenSource;
        }

        ~BaseRequestProcessor()
        {
            _requestCancellationTokenSource.Dispose();
        }

        private static string FormUrlParametersString(IReadOnlyList<string> parameters)
        {
            if (parameters == null) return null;

            var length = parameters.Count;
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append($"/{parameters[i]}");
            }

            return stringBuilder.ToString();
        }

        private Task<HttpResponseMessage> SendRequestToWebServer(CancellationToken cancellationToken,
            TRequestBodyModelInterface requestBodyModel = null, IRequestHeaders requestHeaders = null)
        {
            return ApiHelper.MakeAsyncRequest(cancellationToken, _requestProcessorParameters.RequestMethod,
                _requestProcessorParameters.RequestSuffix, FormUrlParametersString(_requestProcessorParameters.RequestParameters),
                _requestProcessorParameters.QueryStringParameters, requestHeaders?.GetRequestHeaders(), requestBodyModel,
                SendBodyAsQueryStringFormat);
        }


        public struct HttpResponse : ISuccess
        {
            public TResponseModelInterface ResponseModelInterface;
            public HttpHeaders Headers;
            public string ResponsePhrase;
            public string Error;
            public bool Success { get; set; }
            public string ResponseContentAsString { get; set; }
            public HttpStatusCode StatusCode { get; set; }
        }

        public async Task<HttpResponse> SendRequest(string successfulResponseMassage)
        {
            try
            {
                using (var responseMessage = await SendRequestToWebServer(RequestCancellationToken, _requestProcessorParameters.RequestBodyModel,
                    _requestProcessorParameters.RequestHeaders).ConfigureAwait(false))
                {
                    var contentAsString = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    LogUtility.PrintLog(Tag, $"Response String: {contentAsString}");

                    var httpResponse = new HttpResponse
                    {
                        Headers = responseMessage.Headers,
                        StatusCode = responseMessage.StatusCode,
                        ResponsePhrase = responseMessage.ReasonPhrase,
                        Success = responseMessage.IsSuccessStatusCode,
                        ResponseContentAsString = contentAsString
                    };

                    if (responseMessage.IsSuccessStatusCode)
                    {
                        LogUtility.PrintLog(Tag, successfulResponseMassage);
                        httpResponse.ResponseModelInterface = JsonConverterUtility.ConvertJsonString<TResponseModel>(contentAsString);
                    }
                    else
                    {
                        if (responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                        {
                            await SimpleAutofac.GetInstance<ISessionController>().ProcessTokenInvalidationCase().ConfigureAwait(false);
                        }
                        try
                        {
                            httpResponse.Error = ErrorsHandlingUtility.CollectErrors(contentAsString);
                        }
                        catch (Exception)
                        {
                            httpResponse.Error = contentAsString;
                        }

                        LogUtility.PrintLogError(Tag, $"ResponsePhrase: {responseMessage.ReasonPhrase}; Error massage: {httpResponse.Error}");
                        LogUtility.PrintLog(Tag, $"Content string: {contentAsString}");
                    }

                    return httpResponse;
                }
            }
            catch (OperationCanceledException)
            {
                LogUtility.PrintLog(Tag, "Ongoing request was cancelled");
                throw;
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }
        
    }
}