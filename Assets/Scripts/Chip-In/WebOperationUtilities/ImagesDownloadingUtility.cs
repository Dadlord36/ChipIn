using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WebOperationUtilities
{
    public static class ImagesDownloadingUtility
    {
        private const string Tag = "ImagesDownloadingUtility";


        public static async Task<Texture2D> DownloadImageAsync(string url)
        {
            try
            {
                using (var client = UnityWebRequestTexture.GetTexture(url))
                {
                    PrintLog("Image Loading started");
                    var webRequest = client.SendWebRequest();

                    while (!webRequest.isDone)
                    {
                        PrintLog($"ImageLoadingProgress: {webRequest.progress.ToString()}");
                        await Task.Delay(500);
                    }

                    PrintLog("Image Loaded");


                    return DownloadHandlerTexture.GetContent(webRequest.webRequest);
                }
            }
            catch (Exception e)
            {
                Debug.unityLogger.LogException(e);
                throw;
            }
        }

        private static void PrintLog(string message)
        {
            Debug.unityLogger.Log(LogType.Log, Tag, message);
        }

        private static void PrintException(Exception exception)
        {
            Debug.unityLogger.LogException(exception);
        }
    }

    public static class DataDownloadingUtility
    {
        private const string Tag = "DataDownloading";
        public static async Task<byte[]> DownloadRawImageData(string uri)
        {
            using (var myWebClient = new WebClient())
            {
                // Download home page data.
                PrintLog($"Downloading {uri}");
                // Download the Web resource and save it into a data buffer.
                return await myWebClient.DownloadDataTaskAsync(uri);
            }
        }
        
        private static void PrintLog(string message)
        {
            Debug.unityLogger.Log(LogType.Log, Tag, message);
        }
    } 
}