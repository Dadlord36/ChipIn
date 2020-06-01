using Controllers.SlotsSpinningControllers;
using UnityEditor;
using UnityEngine;

namespace Inspectors
{
    [CustomEditor(typeof(ScrollEngineController))]
    public class ScrollEngineControllerEditor : Editor
    {
        private ScrollEngineController _scrollEngineController;

        private void OnEnable()
        {
            _scrollEngineController = target as ScrollEngineController;
        }

        public override void OnInspectorGUI()
        {
            if(GUILayout.Button("Align items"))
            {
                _scrollEngineController.AlignItems();
            }
            base.OnInspectorGUI();
        }
    }
}