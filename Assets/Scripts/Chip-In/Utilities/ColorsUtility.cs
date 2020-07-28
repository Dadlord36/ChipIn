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
                Color.Lerp(a, b, step * i);
            }

            return colors;
        }
    }
}