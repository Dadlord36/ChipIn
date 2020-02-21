using UnityEditor;
using UnityEngine;
using Views.ViewElements.ViewsPlacers;

namespace Inspectors
{
    [CustomEditor(typeof(TwoSlotsViewsPlacer))]
    public class TwoSlotsViewsPlacerEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Create Containers"))
            {
                ((TwoSlotsViewsPlacer) target).Editor_InitializeContainers();
                EditorUtility.SetDirty(target);
            }

            base.OnInspectorGUI();
        }
    }
}