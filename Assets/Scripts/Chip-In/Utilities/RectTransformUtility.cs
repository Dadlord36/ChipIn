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
    }
}