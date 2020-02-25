using UnityEngine;

namespace Common
{
    public struct IndexedTexture
    {
        public readonly Texture2D SpriteSheetTexture;
        public readonly int CorrespondingIndex;

        public IndexedTexture(Texture2D spriteSheetTexture, int correspondingIndex)
        {
            SpriteSheetTexture = spriteSheetTexture;
            CorrespondingIndex = correspondingIndex;
        }
    }
}