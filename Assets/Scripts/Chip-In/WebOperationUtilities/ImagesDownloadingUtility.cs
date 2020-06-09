using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DataModels.Interfaces;
using UnityEngine;
using Utilities;

namespace WebOperationUtilities
{
    public static class ImagesDownloadingUtility
    {
        private const string Tag = "ImagesDownloadingUtility";

        public static Task<Texture2D[]> TryDownloadImagesArray(HttpClient httpClient, string[] imagesUrls)
        {
            try
            {
                var length = imagesUrls.Length;
                var tasks = new Task<Texture2D> [length];
                for (var i = 0; i < length; i++)
                {
                    tasks[i] = TryDownloadImageAsync(httpClient, imagesUrls[i]);
                }

                return Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<Texture2D> TryDownloadImageAsync(HttpClient httpClient, string url)
        {
            try
            {
                Texture2D textureToReturn = null;

                if (string.IsNullOrEmpty(url))
                {
                    throw new Exception("Given url is null or empty");
                }

                await CreateLoadDataTask(httpClient, url).ContinueWith(async delegate(Task<byte[]> task)
                {
                    try
                    {
                        textureToReturn = new Texture2D(0, 0);
                        textureToReturn.LoadImage(await task);
                        textureToReturn.Apply();
                    }
                    catch (Exception e)
                    {
                        LogUtility.PrintLogException(e);
                        throw;
                    }
                }, TaskScheduler.FromCurrentSynchronizationContext()).ConfigureAwait(false);
                return textureToReturn;
            }
            catch (Exception e)
            {
                PrintException(e);
                throw;
            }
        }

        public static Task<byte[]> CreateLoadDataTask(HttpClient httpClient, in string uri)
        {
            return httpClient.GetByteArrayAsync(uri);
        }

        public static Task<byte[][]> CreateDownloadMultipleDataArrayFromUrlsTask(HttpClient httpClient, IReadOnlyList<IUrl> imagesUrls)
        {
            var tasks = new List<Task<byte[]>>(imagesUrls.Count);
            foreach (var url in imagesUrls)
            {
                tasks.Add(CreateLoadDataTask(httpClient, url.Url));
            }

            return Task.WhenAll(tasks);
        }

        private static void PrintLog(string message)
        {
            LogUtility.PrintLog(Tag, message);
        }

        private static void PrintException(Exception exception)
        {
            LogUtility.PrintLogException(exception);
        }
    }
}