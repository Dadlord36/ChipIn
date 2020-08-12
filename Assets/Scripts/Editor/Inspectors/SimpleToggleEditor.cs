using UnityEditor;
using Views.ViewElements;
using Toggle = UnityEngine.UI.Toggle;

namespace Inspectors
{
    [CustomEditor(typeof(SimpleToggle))]
    public class SimpleToggleEditor : Editor
    {
        private SimpleToggle _simpleToggle;
        private Toggle _graphic;

        protected  void OnEnable()
        {
            _simpleToggle = (SimpleToggle) target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            if (_simpleToggle.UiElementsReferencesAreValid)
            {
                _simpleToggle.LabelText = EditorGUILayout.TextField("Label text", _simpleToggle.LabelText);
            }

            if (EditorGUI.EndChangeCheck())
            {
                Undo.RecordObject(_simpleToggle,"Simple toggle property changed");
                EditorUtility.SetDirty(_simpleToggle);
            }

            base.OnInspectorGUI();
        }
    }
}