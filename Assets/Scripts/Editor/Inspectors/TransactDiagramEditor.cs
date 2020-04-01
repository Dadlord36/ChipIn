using UnityEditor;
using UnityEngine;
using ViewModels.Elements;

namespace Inspectors
{
    [CustomEditor(typeof(TransactDiagram))]
    public class TransactDiagramEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Assign"))
            {
                ((TransactDiagram)target).SetDiagramsValues();
            }
            base.OnInspectorGUI();
        }
    }
}