using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

namespace Utilities
{
    public static class JsonConverterUtility
    {
        public static async Task<T> ContentJsonTo<T>(HttpContent content)
        {
            string contentAsString = await content.ReadAsStringAsync();
            return JsonUtility.FromJson<T>(contentAsString);
        }
    }
}