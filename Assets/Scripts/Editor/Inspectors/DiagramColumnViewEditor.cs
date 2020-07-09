using UnityEditor;
using UnityEngine;
using ViewModels.UI;

namespace Inspectors
{
    [CustomEditor(typeof(DiagramColumnView))]
    public class DiagramColumnViewEditor : Editor
    {
        private DiagramColumnView _diagramColumnView;

        private void OnEnable()
        {
            _diagramColumnView = target as DiagramColumnView;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Apply color"))
            {
                _diagramColumnView.ApplyColor();
                EditorUtility.SetDirty(_diagramColumnView);
            }

            base.OnInspectorGUI();
        }
    }
}