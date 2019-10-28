using UnityEngine;

namespace ScriptableObjects.Parameters
{
    public abstract class ScriptableValue<T> : ScriptableObject where T:struct
    {
        public T value;
    }
}