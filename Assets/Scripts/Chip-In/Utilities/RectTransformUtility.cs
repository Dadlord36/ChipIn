using UnityEngine;

namespace Utilities
{
    public static class RectTransformUtility
    {
        public static void Stretch(RectTransform rectTransform)
        {
            rectTransform.anchorMin = Vector2.zero;
            rectTransform.anchorMax = Vector2.one;
        }

        public static void ResetSize(RectTransform rectTransform)
        {
            rectTransform.offsetMin = rectTransform.offsetMax = Vector2.zero;
        }

        public static void ResetScale(RectTransform rectTransform)
        {
            rectTransform.localScale = Vector3.one;
        }

        public static Vector2[] ConvertFromWorldToScreenSpace(Camera camera, Vector2[] vectorsArray)
        {
            var result = new Vector2[vectorsArray.Length];

            for (int i = 0; i < vectorsArray.Length; i++)
            {
                result[i] = UnityEngine.RectTransformUtility.WorldToScreenPoint(camera, vectorsArray[i]);
            }

            return result;
        }
    }
}