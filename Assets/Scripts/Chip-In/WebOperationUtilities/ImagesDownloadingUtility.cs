using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace WebOperationUtilities
{
    public static class ImagesDownloadingUtility
    {
        public static async Task<Texture2D> DownloadImageAsync(string url)
        {
            return await new Task<Texture2D>(() => DownloadImageSync(url));
        }

        public static Texture2D DownloadImageSync(string url)
        {
            using (var client = UnityWebRequestTexture.GetTexture(url))
            {
                return ((DownloadHandlerTexture) client.downloadHandler).texture;
            }
        }
    }
}