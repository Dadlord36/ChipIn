namespace Utilities
{
    public static class PropertiesUtility
    {
        public static string BoolToString(bool value)
        {
            return ToString(value);
        }

        public static string ToString<T>(in T value) where T:struct
        {
            return value.ToString().ToLower();
        }
    }
}