using UnityEditor;

namespace Shapes2D
{
    [CustomEditor(typeof(ShapeWithParametricGradient))]
    public class ShapeWithParametricGradientEditor : ShapeEditor
    {
        public override void OnInspectorGUI()
        {
            
            base.OnInspectorGUI();
        }
    }
}