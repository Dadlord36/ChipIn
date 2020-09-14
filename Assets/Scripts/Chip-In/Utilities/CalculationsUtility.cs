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
    }
}