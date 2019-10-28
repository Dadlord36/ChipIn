using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(ColorParameter), menuName = "Parameters/"+nameof(ColorParameter), order = 0)]
    public class ColorParameter : ScriptableValue<Color>
    {
    }
}