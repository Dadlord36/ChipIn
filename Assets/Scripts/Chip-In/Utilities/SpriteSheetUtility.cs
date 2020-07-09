using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class SpriteSheetUtility
    {
        public static List<Sprite> GetSpritesFromSpritesSheet(Texture2D texture, int rows, int columns)
        {
            int spriteWidth = texture.width / rows;
            int spriteHeight = texture.height / columns;
            var spritesCount = rows * columns;

            var sprites = new List<Sprite>(spritesCount);

            for (int y = 0; y < rows; y++)
            {
                for (int x = columns - 1; x >= 0; x--)
                {
                    var sprite = Sprite.Create(texture, new Rect(x * spriteWidth, y * spriteHeight, spriteWidth, spriteHeight),
                        new Vector2(spriteWidth / 2f, spriteHeight / 2f));
                    sprites.Add(sprite);
                }
            }

            sprites.Reverse();
            return sprites;
        }
    }
}