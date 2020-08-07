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


        public static async Task<Sprite> CreateDownloadImageTask(HttpClient httpClient, TaskFactory mainThreadTaskFactory, string url,
            CancellationToken cancellationToken)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    throw new Exception("Given url is null or empty");
                }

                var responseMessage = await LoadDataAsync(httpClient, url, cancellationToken).ConfigureAwait(false);

                if (!responseMessage.IsSuccessStatusCode)
                {
                    throw new Exception(responseMessage.ReasonPhrase);
                }

                var bytesArray = await responseMessage.Content.ReadAsByteArrayAsync().ConfigureAwait(false);


                return await mainThreadTaskFactory.StartNew(delegate
                {
                    var texture = new Texture2D(0, 0);
                    texture.LoadImage(bytesArray);
                    texture.Apply();
                    return SpritesUtility.CreateSpriteWithDefaultParameters(texture);
                }, cancellationToken).ConfigureAwait(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private static Task<HttpResponseMessage> LoadDataAsync(HttpClient httpClient, in string uri, in CancellationToken cancellationToken)
        {
            return httpClient.GetAsync(uri, HttpCompletionOption.ResponseContentRead, cancellationToken);
        }

        public static Task<byte[][]> CreateDownloadMultipleDataArrayFromUrlsTask(HttpClient httpClient, IReadOnlyList<IUrl>
            imagesUrls, in CancellationToken cancellationToken)
        {
            var tasks = new List<Task<HttpResponseMessage>>(imagesUrls.Count);
            foreach (var url in imagesUrls)
            {
                tasks.Add(LoadDataAsync(httpClient, url.Url, cancellationToken));
            }

            return Task.WhenAll(tasks).ContinueWith(delegate(Task<HttpResponseMessage[]> task)
            {
                var result = task.GetAwaiter().GetResult();
                var bytesTasks = new List<Task<byte[]>>(result.Length);

                for (int i = 0; i < result.Length; i++)
                {
                    bytesTasks.Add(result[i].Content.ReadAsByteArrayAsync());
                }

                return Task.WhenAll(bytesTasks);
            }, cancellationToken, TaskContinuationOptions.OnlyOnRanToCompletion, TaskScheduler.FromCurrentSynchronizationContext()).Unwrap();
        }
    }
}