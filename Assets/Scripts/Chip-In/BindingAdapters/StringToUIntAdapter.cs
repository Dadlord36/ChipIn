using UnityWeld.Binding;

namespace BindingAdapters
{
    [Adapter(typeof(string), typeof(uint))]
    public class StringToUIntAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            if (valueIn == null) return (uint)0;
            if (uint.TryParse((string) valueIn, out var result))
            {
                return result;
            }

            return string.Empty;
        }
    }
}