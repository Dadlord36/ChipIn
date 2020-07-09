using UnityEngine;
using WebOperationUtilities;
using ZXing.Common;

namespace Utilities
{
    public static class QrCodeUtility
    {
        public static Texture2D ConvertBitMatrixToTexture2D(BitMatrix bitMatrix)
        {
            var mCodeTex = new Texture2D(bitMatrix.Width, bitMatrix.Height);
            //set the content pixels in target image
            for (int i = 0; i != mCodeTex.width; i++)
            {
                for (int j = 0; j != mCodeTex.height; j++)
                {
                    mCodeTex.SetPixel(i, j, bitMatrix[i, j] ? Color.black : Color.white);
                }
            }

            return mCodeTex;
        }

        public static Sprite ConvertBitMatrixToSprite(BitMatrix bitMatrix)
        {
            return SpritesUtility.CreateSpriteWithDefaultParameters(ConvertBitMatrixToTexture2D(bitMatrix));
        }
    }
}