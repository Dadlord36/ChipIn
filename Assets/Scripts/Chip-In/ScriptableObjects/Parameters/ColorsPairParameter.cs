using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(ColorsPairParameter), menuName = "Parameters/"+nameof(ColorsPairParameter), order = 0)]
    public class ColorsPairParameter : ScriptableDualValue<Color>
    {
    }
}