using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class SpriteSheetUtility
    {
        public static List<Sprite> GetSprites(Texture2D texture, int spriteWidth, int spriteHeight)
        {
            var pixelsArrays = GetListOfCutPixelsArrays(texture, spriteWidth, spriteHeight);
            var sprites = new List<Sprite>(pixelsArrays.Count);


            for (int i = 0; i < pixelsArrays.Count; i++)
            {
                var tempTexture = new Texture2D(spriteWidth, spriteHeight);
                tempTexture.SetPixels(pixelsArrays[i], 0);
                tempTexture.Apply();

                var sprite = Sprite.Create(tempTexture, new Rect(0f, 0f, spriteWidth, spriteHeight), Vector2.zero);

                sprites.Add(sprite);
            }

            return sprites;
        }

        public static List<Sprite> GetSprites(Texture2D texture, Vector2Int rowsColumns)
        {
            return GetSprites(texture, texture.width / rowsColumns.y, texture.height / rowsColumns.x);
        }

        public static List<Color[]> GetListOfCutPixelsArrays(Texture2D texture, int spriteWidth, int spriteHeight)
        {
            var rows = texture.width / spriteWidth;
            var columns = texture.height / spriteHeight;
            var spritesCount = rows * columns;
            var cutPixelsArrays = new List<Color[]>(spritesCount);

            var cuttingAria = Vector2Int.zero;
            var tempList = new List<Color[]>(columns);

            for (int i = 0; i < rows; i++)
            {
                tempList.Clear();

                for (int j = 0; j < columns; j++)
                {
                    tempList.Add(texture.GetPixels(cuttingAria.x, cuttingAria.y, spriteWidth, spriteHeight));
                    cuttingAria.x += spriteWidth;
                }

                tempList.Reverse();
                cutPixelsArrays.AddRange(tempList);

                cuttingAria.y += spriteHeight;
                cuttingAria.x = 0;
            }

            cutPixelsArrays.Reverse();
            return cutPixelsArrays;
        }
    }
}