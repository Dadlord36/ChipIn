using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using DataModels.Interfaces;
using UnityEngine;
using UnityEngine.Networking;
using Utilities;

namespace WebOperationUtilities
{
    public static class ImagesDownloadingUtility
    {
        private const string Tag = "ImagesDownloadingUtility";

        public static Task<Texture2D[]> TryDownloadImagesArray(string[] imagesUrls)
        {
            try
            {
                var length = imagesUrls.Length;
                var tasks = new Task<Texture2D> [length];
                for (var i = 0; i < length; i++)
                {
                    tasks[i] = TryDownloadImageAsync(imagesUrls[i]);
                }

                return Task.WhenAll(tasks);
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
        }

        public static async Task<Texture2D> TryDownloadImageAsync(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    throw new Exception("Given url is null or empty");
                }

                using (var client = UnityWebRequestTexture.GetTexture(url))
                {
                    PrintLog("Image Loading started");
                    var webRequest = client.SendWebRequest();

                    while (!webRequest.isDone)
                    {
                        PrintLog($"ImageLoadingProgress: {webRequest.progress.ToString()}");
                        await Task.Delay(100);
                    }

                    PrintLog("Image Loaded");


                    return DownloadHandlerTexture.GetContent(webRequest.webRequest);
                }
            }
            catch (Exception e)
            {
                PrintException(e);
                throw;
            }
        }

        public static async Task<List<byte[]>> DownloadMultipleDataArrayFromUrls(IReadOnlyList<IUrl> imagesUrls)
        {
            try
            {
                var tasks = new List<Task<byte[]>>(imagesUrls.Count);
                for (int i = 0; i < imagesUrls.Count; i++)
                {
                    using (var client = new WebClient())
                    {
                        tasks.Add(client.DownloadDataTaskAsync(imagesUrls[i].Url));
                        client.DownloadProgressChanged += (sender, args) => LogUtility.PrintLog(Tag, $"{sender} progress: {args.ProgressPercentage.ToString()}");
                    }
                }

                var bytesArray = await Task.WhenAll(tasks);
                return bytesArray.ToList();
            }
            catch (Exception e)
            {
                LogUtility.PrintLogException(e);
                throw;
            }
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

    public static class DataDownloadingUtility
    {
        private const string Tag = "DataDownloading";

        public static Task<byte[]> DownloadRawImageData(string uri)
        {
            using (var myWebClient = new WebClient())
            {
                // Download home page data.
                PrintLog($"Downloading {uri}");
                // Download the Web resource and save it into a data buffer.
                return myWebClient.DownloadDataTaskAsync(uri);
            }
        }

        private static void PrintLog(string message)
        {
            LogUtility.PrintLog(Tag, message);
        }
    }
}