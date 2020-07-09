using UnityEditor;
using UnityEngine;
using ViewModels.UI.Elements.Buttons;

namespace Inspectors
{
    [CustomEditor(typeof(BarButtonSelection))]
    public class BarButtonSelectionEditor : Editor
    {
        private BarButtonSelection _barButtonSelection;

        private void OnEnable()
        {
            _barButtonSelection = target as BarButtonSelection;
        }

        public override void OnInspectorGUI()
        {
            _barButtonSelection.shouldShowReferencesFields = EditorGUILayout.ToggleLeft(
                "Show UI elements references fields",
                _barButtonSelection.shouldShowReferencesFields);


            EditorGUI.BeginChangeCheck();

            EditorGUILayout.PropertyField(serializedObject.FindProperty("onClick"));
            
            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                MakeTargetDirty();
            }

            if (_barButtonSelection.IsIconValid)
            {
                EditorGUI.BeginChangeCheck();
                _barButtonSelection.Icon =
                    EditorGUILayout.ObjectField("Icon sprite", _barButtonSelection.Icon, typeof(Sprite), false) as
                        Sprite;
                _barButtonSelection.IconSize = EditorGUILayout.Vector2Field("Icon size", _barButtonSelection.IconSize);
                if (EditorGUI.EndChangeCheck())
                {
                    MakeTargetDirty();
                }
            }

 

            if (!_barButtonSelection.shouldShowReferencesFields) return;
            base.OnInspectorGUI();
        }

        private void MakeTargetDirty()
        {
            EditorUtility.SetDirty(target);
        }
    }
}