using UnityWeld.Binding;

namespace BindingAdapters
{
    [Adapter(typeof(int), typeof(uint))]
    public class IntToUintAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            return (uint) valueIn;
        }
    }
}