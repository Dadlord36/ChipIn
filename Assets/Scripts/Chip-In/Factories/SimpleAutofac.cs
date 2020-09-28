using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Factories
{
    public static class SimpleAutofac
    {
        private static readonly List<object> Objects = new List<object>();

        public static void AddObjectInstance(object objectInstance)
        {
            Objects.Add(objectInstance);
        }

        public static T GetInstance<T>() where T:class
        {
            var result = Objects.Find(o => o is T);
           Assert.IsNotNull(result);
           return result as T;
        }
    }
}