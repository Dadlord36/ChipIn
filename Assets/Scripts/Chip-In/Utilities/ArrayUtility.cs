using System.Collections.Generic;
using UnityEngine;

namespace Utilities
{
    public static class ArrayUtility
    {
        /// <summary>
        /// Allows to treat 1D array as 2D array, so that all items will be in stack as opposite to storing them in
        /// 2D array, where each row is a new 1D array, stored in different heap location. First coordinate is (X=0;Y=0)
        /// </summary>
        /// <param name="x">X coordinate (column)</param>
        /// <param name="y">Y coordinate (row)</param>
        /// <param name="arrayWidth">Width is </param>
        /// <returns>Element at given x,y coordinate index</returns>
        public static int GetAtIndex(int x, int y, int arrayWidth)
        {
            return x + y * arrayWidth;
        }

        /// <summary>
        /// Converts 2D array coordinate into 1D array index
        /// </summary>
        /// <param name="coordinates">X - column, Y - rows</param>
        /// <param name="arrayWidth"></param>
        /// <returns></returns>
        public static int GetAtIndex(in Vector2Int coordinates, in int arrayWidth)
        {
            return GetAtIndex(coordinates.x, coordinates.y, arrayWidth);
        }

        public static Vector2Int Get2DCoordinateFromIndex(int index, int arrayWidth)
        {
            return new Vector2Int(index % arrayWidth, index / arrayWidth);
        }

        public static List<T> GetRemainArrayItemsStartingWithIndex<T>(List<T> items, int startingIndex,
            uint lengthLimit)
        {
            return items.GetRange(startingIndex, (int) Mathf.Clamp(items.Count - startingIndex, 0, lengthLimit));
        }

        public static T GetMiddleElement<T>(IReadOnlyList<T> ia)
        {
            return ia[CalculationsUtility.GetMiddle(ia.Count)];
        }
    }
}