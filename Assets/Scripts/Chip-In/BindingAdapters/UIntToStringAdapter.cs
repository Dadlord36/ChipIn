using System;
using UnityWeld.Binding;

namespace BindingAdapters
{
    [Adapter(typeof(UInt32), typeof(String))]
    public class UIntToStringAdapter : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            return valueIn.ToString();
        }
    }
}