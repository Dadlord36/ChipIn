using UnityWeld.Binding;

namespace BindingAdapters
{
    [Adapter(typeof(int), typeof(bool))]
    public class IntToBoolAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            return (int) valueIn != 0;
        }
    }
}