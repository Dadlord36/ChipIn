﻿using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Utilities
{
    public static class JsonConverterUtility
    {
        public static async Task<T> ContentAsyncJsonTo<T>(HttpContent content)
        {
            string contentAsString = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(contentAsString);
        }
    }
}