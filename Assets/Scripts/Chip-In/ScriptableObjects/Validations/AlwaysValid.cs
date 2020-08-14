using UnityEngine;

namespace ScriptableObjects.Validations
{
    [CreateAssetMenu(fileName = nameof(AlwaysValid), menuName = nameof(Validations) + "/" + nameof(AlwaysValid),
        order = 0)]
    public class AlwaysValid : TextValidation
    {
        public override bool CheckIsValid(object dataToValidate)
        {
            return true;
        }
    }
}