using UnityEngine;

namespace Utilities
{
    public static class ColorsUtility
    {
        public static Color[] GenerateColorsBetweenTwColors(Color a, Color b, int itemsNumber)
        {
            var colors = new Color[itemsNumber];
            var step = 1f / itemsNumber;
            for (int i = 0; i < itemsNumber; i++)
            {
                colors[i] = CreateColorFromHSV(Mathf.Lerp(startValue, endValue, step * i));
                colors[i].a = alpha;
            }

            return colors;
        }
    }
}