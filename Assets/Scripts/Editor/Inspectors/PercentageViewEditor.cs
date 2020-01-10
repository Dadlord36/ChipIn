using UI.Elements;
using UnityEditor;

namespace Inspectors
{
    [CustomEditor(typeof(PercentageView))]
    public class PercentageViewEditor : Editor
    {
        private PercentageView _percentageView;

        private void OnEnable()
        {
            _percentageView = (PercentageView) target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            EditorGUI.BeginChangeCheck();
            _percentageView.Percentage = EditorGUILayout.Slider(_percentageView.Percentage, 0f, 1.0f);
            if (EditorGUI.EndChangeCheck())
            {
                
            }
        }
    }
}