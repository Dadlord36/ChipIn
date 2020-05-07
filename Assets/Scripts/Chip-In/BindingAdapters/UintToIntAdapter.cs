using UnityWeld.Binding;

namespace BindingAdapters
{
    [Adapter(typeof(uint), typeof(int))]
    public class UintToIntAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            return valueIn==null ? 0 :  (int) valueIn;
        }
    }
}