using System;

namespace Utilities
{
    public static class CalculationsUtility
    {
        public static int GetMiddle(int number)
        {
            // if length is odd, return middle numer
            if (number % 2 == 1) return number/2;

            // if length is even return the first of the two middle numbers
            return number/2  - 1;
        }
        
        public static T Clamp<T>(T val, T min, T max) where T : IComparable<T>
        {
            if (val.CompareTo(min) < 0) return min;
            else if(val.CompareTo(max) > 0) return max;
            else return val;
        }
    }
}