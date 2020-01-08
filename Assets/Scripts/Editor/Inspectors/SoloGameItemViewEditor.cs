using UnityEditor;
using UnityEngine;
using Views;

namespace Inspectors
{
    [CustomEditor(typeof(SoloGameItemView))]
    public class SoloGameItemViewEditor : Editor
    {
        private SoloGameItemView _soloGameItemView;
        private bool _showFields;

        private void OnEnable()
        {
            _soloGameItemView = (SoloGameItemView) target;
        }

        public override void OnInspectorGUI()
        {
            _showFields = EditorGUILayout.Toggle("Show references fields", _showFields);

            if (_soloGameItemView.AllUiElementsAreSets && !_showFields)
            {
                _soloGameItemView.GameName = EditorGUILayout.TextField(_soloGameItemView.GameName);
                _soloGameItemView.GameTypeName = EditorGUILayout.TextField(_soloGameItemView.GameTypeName);
                _soloGameItemView.GameTypeIcon =
                    (Sprite) EditorGUILayout.ObjectField(_soloGameItemView.GameTypeIcon, typeof(Sprite), false);
                _soloGameItemView.ButtonImageVisible = EditorGUILayout.Toggle("Show button background",
                    _soloGameItemView.ButtonImageVisible);

                _soloGameItemView.ButtonSize =
                    EditorGUILayout.Vector2Field("Button size", _soloGameItemView.ButtonSize);
                _soloGameItemView.ButtonImageSize =
                    EditorGUILayout.Vector2Field("Button Image size", _soloGameItemView.ButtonImageSize);


                if (GUI.changed)
                {
                    _soloGameItemView.RefreshUiElements();
                }
            }
            else
            {
                base.OnInspectorGUI();
            }
        }
    }
}