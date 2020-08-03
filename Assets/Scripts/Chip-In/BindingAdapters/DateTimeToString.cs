using System;
using UnityWeld.Binding;

namespace BindingAdapters
{
    [Adapter(typeof(DateTime), typeof(string))]
    public class DateTimeToShortDataString : IAdapter
    {
        public object Convert(object valueIn, AdapterOptions options)
        {
            return ((DateTime) valueIn).ToShortDateString();
        }
    }
}