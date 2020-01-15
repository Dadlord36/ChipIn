using System;
using UnityEngine;

namespace WebOperationUtilities
{
    public static class SpritesUtility
    {
        private const string Tag = nameof(SpritesUtility);
        public static Sprite CreateSpriteWithDefaultParameters(Texture2D texture)
        {
            return Sprite.Create(texture, new Rect(Vector2.zero, new Vector2(texture.width, texture.height)),
                Vector2.zero);
        }
        
        public static Texture2D CreateNonReadableTextureFromData(byte[] rawData)
        {
            var texture = new Texture2D(1, 1);
            texture.LoadImage(rawData,true);
            return texture;
        }

        public static Sprite CreateSpriteWithDefaultParameters(byte[] rawData)
        {
            return CreateSpriteWithDefaultParameters(CreateNonReadableTextureFromData(rawData));
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
}