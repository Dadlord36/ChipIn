using UnityEngine;

namespace ScriptableObjects.Parameters
{
    public abstract class ScriptableDualValue<T> : ScriptableObject where T:struct
    {
        public T value1;
        public T value2;
    }
}