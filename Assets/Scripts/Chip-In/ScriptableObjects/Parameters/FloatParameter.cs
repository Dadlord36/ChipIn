using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(FloatParameter),
        menuName = "Parameters/" + nameof(FloatParameter), order = 0)]
    class FloatParameter : ScriptableValue<float>
    {
    }
}