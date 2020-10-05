using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Factories
{
    public class SimpleAutofacContainer
    {
        private readonly List<object> Objects = new List<object>();

        public void AddObjectInstance(object objectInstance)
        {
            Objects.Add(objectInstance);
        }

        public T GetObjectInstance<T>() where T : class
        {
            var result = Objects.Find(o => o is T);
            Assert.IsNotNull(result);
            return result as T;
        }
    }
}