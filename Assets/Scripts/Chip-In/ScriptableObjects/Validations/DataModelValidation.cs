using UnityEngine;

namespace ScriptableObjects.Validations
{
    /// <summary>
    /// Base class for any data models validation rule-sets
    /// </summary>
    /// <typeparam name="T">Data Model type</typeparam>
    public abstract class DataModelValidation<T> : ScriptableObject
    {
        public abstract bool CheckIsValid(T dataModel);
    }
}