using Controllers;
using UnityEditor;
using UnityEngine;

namespace Inspectors
{
    [CustomEditor(typeof(SlotSpinner))]
    public class SlotsSpinnerEditor : Editor
    {
        private SlotSpinner _slotSpinner;


        private void OnEnable()
        {
            _slotSpinner = target as SlotSpinner;
        }

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Align Items"))
            {
                _slotSpinner.AlignItems();
            }

            if (GUILayout.Button("Start spinning"))
            {
                _slotSpinner.StartSpinning();
            }

            base.OnInspectorGUI();
        }
    }
}