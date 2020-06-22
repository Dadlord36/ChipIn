using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using DataModels.Interfaces;
using UnityEngine;

namespace WebOperationUtilities
{
    public static class ImagesDownloadingUtility
    {
        private const string Tag = "ImagesDownloadingUtility";

        public static Task<Texture2D[]> CreateDownloadImagesArrayTask(HttpClient httpClient, TaskScheduler mainThreadScheduler,
            string[] imagesUrls, in CancellationToken cancellationToken)
        {
            var length = imagesUrls.Length;
            var tasks = new Task<Texture2D> [length];
            for (var i = 0; i < length; i++)
            {
                tasks[i] = CreateDownloadImageTask(httpClient, mainThreadScheduler, imagesUrls[i], cancellationToken);
            }

            return Task.WhenAll(tasks);
        }

        public class DataDownloadingFailureException : Exception
        {
            public DataDownloadingFailureException() : base("Bytes downloading was probably cancelled and some socket related exception occur")
            {
            }
        }

        public static Task<Texture2D> CreateDownloadImageTask(HttpClient httpClient, TaskScheduler mainThreadScheduler, string url,
            CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new Exception("Given url is null or empty");
            }

            return CreateLoadDataTask(httpClient, url, cancellationToken).ContinueWith(
                delegate(Task<HttpResponseMessage> loadDataResponseTask)
                {
                    if (loadDataResponseTask.IsCanceled)
                    {
                        cancellationToken.ThrowIfCancellationRequested();
                    }
                    if (loadDataResponseTask.IsFaulted)
                    {
                        throw new DataDownloadingFailureException();
                    }

                    var resultMessage = loadDataResponseTask.Result;
                    var readBytesTask = resultMessage.Content.ReadAsByteArrayAsync();

                    return readBytesTask.ContinueWith(delegate(Task<byte[]> loadBytesTask)
                    {
                        if (loadBytesTask.IsCanceled)
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                        }

                        var bytesArray = loadBytesTask.Result;
                        var textureToReturn = new Texture2D(0, 0);
                        textureToReturn.LoadImage(bytesArray);
                        textureToReturn.Apply();
                        return textureToReturn;
                    }, cancellationToken, TaskContinuationOptions.None, mainThreadScheduler);
                }, cancellationToken, TaskContinuationOptions.None, mainThreadScheduler).Unwrap();
        }
        
        private static Task<HttpResponseMessage> CreateLoadDataTask(HttpClient httpClient, in string uri,
            in CancellationToken cancellationToken)
        {
            return httpClient.GetAsync(uri, HttpCompletionOption.ResponseContentRead, cancellationToken);
        }

        public static Task<Task<Task<byte[][]>>> CreateDownloadMultipleDataArrayFromUrlsTask(HttpClient httpClient, IReadOnlyList<IUrl>
            imagesUrls, in CancellationToken cancellationToken)
        {
            var tasks = new List<Task<HttpResponseMessage>>(imagesUrls.Count);
            foreach (var url in imagesUrls)
            {
                tasks.Add(CreateLoadDataTask(httpClient, url.Url, cancellationToken));
            }

            var loadIconsTasks = Task.WhenAll(tasks).ContinueWith(async delegate(Task<HttpResponseMessage[]> task)
            {
                var texturesLoadingResponseMassages = await task.ConfigureAwait(false);
                var bytesTasks = new List<Task<byte[]>>(texturesLoadingResponseMassages.Length);

                for (int i = 0; i < texturesLoadingResponseMassages.Length; i++)
                {
                    bytesTasks.Add(texturesLoadingResponseMassages[i].Content.ReadAsByteArrayAsync());
                }

                return Task.WhenAll(bytesTasks);
            }, cancellationToken);
            return loadIconsTasks;
        }
    }
}