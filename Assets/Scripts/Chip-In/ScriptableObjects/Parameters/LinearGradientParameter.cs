using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(LinearGradientParameter),menuName = "Parameters/" + nameof(LinearGradientParameter), order = 0)]
    public class LinearGradientParameter : ScriptableObject
    {
        public Color color1 = Color.white;
        public Color color2 = Color.white;
        [Range(-180f, 180f)]
        public float angle = 0f;
        public bool ignoreRatio = true;
    }
}