using System.Collections.Generic;

namespace Utilities
{
    public static class DictionaryExtensions
    {
        public static void AddOrUpdate(this Dictionary<string,string> dictionary,in string value,in string propertyName)
        {
            if (dictionary.ContainsKey(propertyName))
            {
                dictionary[propertyName] = value;
                return;
            }

            dictionary.Add(propertyName, value);
        }
    }
}