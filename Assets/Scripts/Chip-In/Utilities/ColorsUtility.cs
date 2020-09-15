using UnityEngine;

namespace Utilities
{
    public static class ColorsUtility
    {
        /// <summary>
        /// Creates colors array of shades from start to finish colors.
        /// </summary>
        /// <param name="startValue">Start color value, given as H of HSV parameters</param>
        /// <param name="endValue">End color value, given as H of HSV parameters</param>
        /// <param name="alpha">Color alpha</param>
        /// <param name="shadesNumber">Number of shades (steps)</param>
        /// <returns>Array of produced colors</returns>
        public static Color[] GenerateColorsBetweenTwoColors(float startValue, float endValue, float alpha, int shadesNumber)
        {
            var colors = new Color[shadesNumber];
            var step = 1f / shadesNumber;
            for (int i = 0; i < shadesNumber; i++)
            {
                colors[i] = CreateColorFromHSV(Mathf.Lerp(startValue, endValue, step * i));
                colors[i].a = alpha;
            }

            return colors;
        }

        /// <summary>
        /// Creates Color from H value of HSV parameters
        /// </summary>
        /// <param name="h">Value, that will be clamped between 0..1</param>
        /// <returns>Created color</returns>
        public static Color CreateColorFromHSV(in float h)
        {
            return Color.HSVToRGB(Mathf.Clamp01(h), 1f, 1f);
        }
    }
}