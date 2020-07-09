using Controllers.SlotsSpinningControllers;
using UnityEditor;
using UnityEngine;

namespace Inspectors
{
    [CustomEditor(typeof(SlotSpinningController))]
    public class SlotSpinningControllerEditor : Editor
    {
        private LineEngineController _lineEngineController;


        private void OnEnable()
        {
            _lineEngineController = target as LineEngineController;
        }

        private uint _selectedNumber;

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Align Items"))
            {
                _lineEngineController.AlignItems();
            }

            _selectedNumber = (uint) EditorGUILayout.IntField((int) _selectedNumber);

            if (GUILayout.Button("Slide to selected index"))
            {
                _lineEngineController.SlideInstantlyToIndexPosition(_selectedNumber);
            }
            
            if (GUILayout.Button("Slide to selected ICON index"))
            {
                _lineEngineController.SlideInstantlyToPositionByIconId(_selectedNumber);
            }

            if (GUILayout.Button("Start spinning"))
            {
                if (!Application.isPlaying) return;
                _lineEngineController.StartMovement();
            }

            base.OnInspectorGUI();
        }
    }
}