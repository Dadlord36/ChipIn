using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WebOperationUtilities
{
    public static class ImagesDownloadingUtility
    {
        public static async Task<Texture2D> DownloadImageAsync(string url)
        {
            return await DownloadImageSync(url);
        }

        public static async Task<Texture2D> DownloadImageSync(string url)
        {
            try
            {
                using (var client = UnityWebRequestTexture.GetTexture(url))
                {
                    Debug.Log("Image Loading started");
                    var webRequest = client.SendWebRequest();
                    
                    while (!webRequest.isDone)
                    {
                        Debug.Log($"ImageLoadingProgress: {webRequest.progress.ToString()}");
                        await Task.Delay(500);
                    }

                    Debug.Log("Image Loaded");
                    return DownloadHandlerTexture.GetContent(webRequest.webRequest);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                throw;
            }
        }
    }
}