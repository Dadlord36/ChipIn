using System.Collections.Generic;
using UnityEngine.Assertions;

namespace Factories
{
    public interface IFactoryContainer
    {
        void AddObjectInstanceAs<T>(object objectInstance) where T : class;
        void AddObjectInstanceAs<T, I>() where T : I, new();
        T GetInstance<T>() where T : class;
    }

    public class FactoryContainer : IFactoryContainer
    {
        private readonly List<object> _objects = new List<object>();

        public void AddObjectInstanceAs<T>(object objectInstance) where T : class
        {
            Assert.IsTrue(objectInstance is T);
            _objects.Add((T) objectInstance);
        }

        public void AddObjectInstanceAs<T, I>() where T : I, new()
        {
            _objects.Add(new T());
        }

        public T GetInstance<T>() where T : class
        {
            var result = _objects.Find(o => o is T);
            Assert.IsNotNull(result);
            return result as T;
        }
    }
}