using Controllers.SlotsSpinningControllers;
using Controllers.SlotsSpinningControllers.RecyclerView;
using UnityEditor;
using UnityEngine;

namespace Inspectors
{
    [CustomEditor(typeof(RecyclerView))]
    public class ScrollEngineControllerEditor : Editor
    {
        private RecyclerView _recyclerView;

        private void OnEnable()
        {
            _recyclerView = target as RecyclerView;
        }

        public override void OnInspectorGUI()
        {
            if(GUILayout.Button("Align items"))
            {
                _recyclerView.AlignItems();
            }
            base.OnInspectorGUI();
        }
    }
}