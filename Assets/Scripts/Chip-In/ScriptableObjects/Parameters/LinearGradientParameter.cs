using UnityEngine;

namespace ScriptableObjects.Parameters
{
    [CreateAssetMenu(fileName = nameof(LinearGradientParameter),
        menuName = "Parameters/" + nameof(LinearGradientParameter), order = 0)]
    public class LinearGradientParameter : ScriptableObject
    {
        [SerializeField] private ColorParameter color1;
        [SerializeField] public ColorParameter color2;

        [Range(-180f, 180f)] public float angle = 0f;
        public bool ignoreRatio = true;

        public Color Color1 => color1.value;
        public Color Color2 => color2.value;
    }
}