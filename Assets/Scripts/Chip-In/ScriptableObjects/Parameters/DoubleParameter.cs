using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(DoubleParameter),
        menuName = nameof(ScriptableObjects.Parameters) + "/" + nameof(DoubleParameter), order = 0)]
    class DoubleParameter : ScriptableValue<double>
    {
    }
}