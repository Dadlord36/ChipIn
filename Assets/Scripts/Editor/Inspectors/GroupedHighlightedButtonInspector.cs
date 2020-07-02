using ScriptableObjects.Parameters;
using TMPro;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using ViewModels.UI.Elements.Buttons;

namespace Inspectors
{
    [CustomEditor(typeof(GroupedHighlightedButton))]
    public class GroupedHighlightedButtonInspector : ButtonEditor
    {
        private SerializedProperty _shouldStayHighlighted;
        private SerializedProperty _buttonText;

        private SerializedProperty _normalTextColor;
        private SerializedProperty _highlightedTextColor;


        protected override void OnEnable()
        {
            base.OnEnable();
            var groupedHighlightedButton = (GroupedHighlightedButton) target;

            _shouldStayHighlighted = serializedObject.FindProperty(groupedHighlightedButton.ShouldStayHighlightedName);
            _buttonText = serializedObject.FindProperty(groupedHighlightedButton.TextFieldName);
            _normalTextColor = serializedObject.FindProperty(groupedHighlightedButton.NormalTextColorFieldName);
            _highlightedTextColor =
                serializedObject.FindProperty(groupedHighlightedButton.HighlightedTextColorFieldName);
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUI.BeginChangeCheck();

            _buttonText.objectReferenceValue =
                EditorGUILayout.ObjectField(_buttonText.objectReferenceValue, typeof(TMP_Text), true);

            _normalTextColor.objectReferenceValue = EditorGUILayout.ObjectField(_normalTextColor.objectReferenceValue,
                typeof(ColorParameter), false);
            _highlightedTextColor.objectReferenceValue = EditorGUILayout.ObjectField(
                _highlightedTextColor.objectReferenceValue,
                typeof(ColorParameter), false);

            _shouldStayHighlighted.boolValue =
                EditorGUILayout.Toggle("Should stay highlighted", _shouldStayHighlighted.boolValue);

            if (EditorGUI.EndChangeCheck())
            {
                serializedObject.ApplyModifiedProperties();
                EditorUtility.SetDirty(target);
            }

            base.OnInspectorGUI();
        }
    }
}