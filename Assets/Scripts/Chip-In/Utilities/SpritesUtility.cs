using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Utilities;

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

        public static Sprite[] CreateArrayOfSpritesWithDefaultParameters(Texture2D[] textures)
        {
            var length = textures.Length;

            var sprites = new Sprite[length];
            for (int i = 0; i < length; i++)
            {
                sprites[i] = CreateSpriteWithDefaultParameters(textures[i]);
            }

            return sprites;
        }

        public static Texture2D CreateNonReadableTextureFromData(byte[] rawData)
        {
            var texture = new Texture2D(1, 1);
            texture.LoadImage(rawData, true);
            return texture;
        }

        public static Sprite CreateSpriteWithDefaultParameters(byte[] rawData)
        {
            return CreateSpriteWithDefaultParameters(CreateNonReadableTextureFromData(rawData));
        }

        public static Task<Texture2D> CreateTexture2DFromPathAsync(string pathToImage, TaskScheduler mainThreadTaskScheduler)
        {
            return FilesUtility.ReadFileBytesAsync(pathToImage).ContinueWith(finishedTask => 
                CreateNonReadableTextureFromData(finishedTask.GetAwaiter().GetResult()), 
                CancellationToken.None,TaskContinuationOptions.OnlyOnRanToCompletion, mainThreadTaskScheduler);
        }

        private static void PrintLog(string message)
        {
            Debug.unityLogger.Log(LogType.Log, Tag, message);
        }

        private static void PrintException(Exception exception)
        {
            Debug.unityLogger.LogException(exception);
        }

        public static void MakeTransparent(SpriteRenderer renderer)
        {
            var newColor = renderer.color;
            newColor.a = 0;
            renderer.color = newColor;
        }

        public static bool MakeTransparent(GameObject gameObject)
        {
            var renderer = gameObject.GetComponent<SpriteRenderer>();
            if (!renderer)
                return false;
            MakeTransparent(renderer);
            return true;
        }

        public static T[] GetIndexesOfFragment<T>(T[] sourceArray, int sourceArrayWidth, in Vector2Int leftTopCoordinate,
            in Vector2Int rightBottomCoordinate)
        {
            GetFragmentWidthHeight(leftTopCoordinate, rightBottomCoordinate, out int width, out var height);
            var outputArray = new T[width * height];

            var coordinate = Vector2Int.zero;
            int index = 0;
            for (var y = leftTopCoordinate.y; y < rightBottomCoordinate.y; y++)
            {
                coordinate.y = y;
                for (var x = leftTopCoordinate.x; x < rightBottomCoordinate.x; x++)
                {
                    coordinate.x = x;
                    var matrixCoordinate = ArrayUtility.GetAtIndex(coordinate.y, coordinate.x, sourceArrayWidth);
                    outputArray[index] = sourceArray[matrixCoordinate];
                    index++;
                }
            }

            return outputArray;
        }

        static void GetFragmentWidthHeight(Vector2Int rightBottomIndex,
            Vector2Int leftTopIndex, out int width, out int height)
        {
            width = rightBottomIndex.x + leftTopIndex.x;
            height = rightBottomIndex.y + leftTopIndex.y;
        }
    }
}