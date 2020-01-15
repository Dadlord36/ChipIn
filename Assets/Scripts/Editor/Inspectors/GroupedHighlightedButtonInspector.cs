using ScriptableObjects.Parameters;
using TMPro;
using UnityEditor;
using UnityEditor.UI;
using ViewModels.UI.Elements.Buttons;

namespace Inspectors
{
    [CustomEditor(typeof(GroupedHighlightedButton))]
    public class GroupedHighlightedButtonInspector : ButtonEditor
    {
        private SerializedProperty _buttonText;

        private SerializedProperty _normalFont;
        private SerializedProperty _highlightedFont;

        private SerializedProperty _normalTextColor;
        private SerializedProperty _highlightedTextColor;

        protected override void OnEnable()
        {
            base.OnEnable();
            var groupedHighlightedButton = (GroupedHighlightedButton) target;

            _buttonText = serializedObject.FindProperty(groupedHighlightedButton.TextFieldName);
            _normalFont = serializedObject.FindProperty(groupedHighlightedButton.NormalFontFieldName);
            _highlightedFont = serializedObject.FindProperty(groupedHighlightedButton.HighlightedFontFieldName);
            _normalTextColor = serializedObject.FindProperty(groupedHighlightedButton.NormalTextColorFieldName);
            _highlightedTextColor =
                serializedObject.FindProperty(groupedHighlightedButton.HighlightedTextColorFieldName);
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            
            _buttonText.objectReferenceValue =
                EditorGUILayout.ObjectField(_buttonText.objectReferenceValue, typeof(TMP_Text), true);
            _normalFont.objectReferenceValue =
                EditorGUILayout.ObjectField(_normalFont.objectReferenceValue, typeof(TMP_FontAsset), true);
            _highlightedFont.objectReferenceValue = EditorGUILayout.ObjectField(_highlightedFont.objectReferenceValue,
                typeof(TMP_FontAsset), true);
            
            _normalTextColor.objectReferenceValue = EditorGUILayout.ObjectField(_normalTextColor.objectReferenceValue, 
                typeof(ColorParameter), false);
            _highlightedTextColor.objectReferenceValue = EditorGUILayout.ObjectField(_highlightedTextColor.objectReferenceValue, 
                typeof(ColorParameter), false);
            
            if (EditorGUI.EndChangeCheck())
            {
              serializedObject.Update();  
            }

            base.OnInspectorGUI();
        }
    }
}