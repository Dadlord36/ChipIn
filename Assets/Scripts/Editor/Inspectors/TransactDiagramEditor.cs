using UnityEditor;
using UnityEngine;
using ViewModels.Elements;

namespace Inspectors
{
    [CustomEditor(typeof(TransactDiagram))]
    public class TransactDiagramEditor : Editor
    {
        private TransactDiagram _transactDiagram;

        private void OnEnable()
        {
            _transactDiagram = target as TransactDiagram;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Assign"))
            {
                ((TransactDiagram)target).SetRandomDiagramsValues();
            }

            if (GUILayout.Button("Redraw axis"))
            {
                _transactDiagram.DrawAxis();
                EditorUtility.SetDirty(_transactDiagram);
            }
            if (GUILayout.Button("Insert minimal values"))
            {
                _transactDiagram.InsertMinimalValues();
                EditorUtility.SetDirty(_transactDiagram);
            }
            if (GUILayout.Button("Insert maximal values"))
            {
                _transactDiagram.InsertMaximalValues();
                EditorUtility.SetDirty(_transactDiagram);
            }
            
            base.OnInspectorGUI();
        }
    }
}