using DataModels.Interfaces;
using UnityEngine;

namespace Common
{
    public struct IndexedTexture : IIdentifier
    {
        public readonly Texture2D SpriteSheetTexture;
        public int? Id { get; set; }

        public IndexedTexture(Texture2D spriteSheetTexture, int? correspondingIndex)
        {
            SpriteSheetTexture = spriteSheetTexture;
            Id = correspondingIndex;
        }

        public IndexedTexture(byte[] spriteSheetTextureRawData, int correspondingIndex, int width, int height,TextureFormat textureFormat)
        {
            var texture = new Texture2D(width, height, textureFormat, false);
            texture.LoadRawTextureData(spriteSheetTextureRawData);
            texture.Apply();
            SpriteSheetTexture = texture;
            Id = correspondingIndex;
        }
    }
}