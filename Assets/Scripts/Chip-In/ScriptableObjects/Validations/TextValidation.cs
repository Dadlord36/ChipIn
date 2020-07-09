using UnityEngine;

namespace ScriptableObjects.Validations
{
    public abstract class TextValidation : ScriptableObject
    {
        public abstract bool CheckIsValid(object dataToValidate);
    }
}