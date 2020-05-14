using Controllers.SlotsSpinningControllers;
using UnityEditor;
using UnityEngine;

namespace Inspectors
{
    [CustomEditor(typeof(SlotSpinnerController))]
    public class SlotsSpinnerControllerEditor : Editor
    {
        private SlotSpinnerController _slotSpinnerController;


        private void OnEnable()
        {
            _slotSpinnerController = target as SlotSpinnerController;
        }

        private uint _selectedNumber;

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Align Items"))
            {
                _slotSpinnerController.AlignItems();
            }

            _selectedNumber = (uint) EditorGUILayout.IntField((int) _selectedNumber);

            if (GUILayout.Button("Slide to selected index"))
            {
                _slotSpinnerController.SlideInstantlyToIndexPosition(_selectedNumber);
            }

            if (GUILayout.Button("Start spinning"))
            {
                if (!Application.isPlaying) return;
                _slotSpinnerController.StartElementsSpinning();
            }

            base.OnInspectorGUI();
        }
    }
}