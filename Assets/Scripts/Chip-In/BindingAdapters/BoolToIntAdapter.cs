using UnityWeld.Binding;

namespace BindingAdapters
{
    [Adapter(typeof(bool), typeof(int))]
    public class BoolToIntAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            return (bool) valueIn ? 1 : 0;
        }
    }
}