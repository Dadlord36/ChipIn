using UnityEngine;

namespace Common.ValueGenerators
{
    public static class SimpleValueGenerator
    {
        public static int GenerateIntValueInclusive(int min, int max)
        {
            return Random.Range(min, max+1);
        }
    }
}