using UnityEditor;
using UnityEngine;
using ViewModels.UI.Elements;
using Views.ViewElements;

namespace Inspectors
{
    [CustomEditor(typeof(RadarView))]
    public class RadarViewEditor : Editor
    {
        private RadarView _analyticView;

        private void OnEnable()
        {
            _analyticView = (RadarView) target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            _analyticView.circlesThickness =
                (uint) EditorGUILayout.IntField("Circle thickness", (int) _analyticView.circlesThickness);
            _analyticView.arcSteps =
                (uint) EditorGUILayout.IntField("Arc steps", (int) _analyticView.arcSteps);
            _analyticView.circlesColor = EditorGUILayout.ColorField("Circles color", _analyticView.circlesColor);
            _analyticView.scaleFactor = EditorGUILayout.Slider("Scale factor", _analyticView.scaleFactor, 0f, 1f);
            _analyticView.circlesBaseSize = EditorGUILayout.IntField("Circles size", _analyticView.circlesBaseSize);

            void UpdateCircles()
            {
                _analyticView.SetCirclesStyle(false, _analyticView.circlesBaseSize,_analyticView.arcSteps, _analyticView.circlesThickness,
                    _analyticView.circlesColor);
                _analyticView.ScaleCircles(_analyticView.scaleFactor);
            }

            if (EditorGUI.EndChangeCheck())
            {
                UpdateCircles();
                EditorUtility.SetDirty(_analyticView);
            }

            if (GUILayout.Button("Add circle"))
            {
                _analyticView.AddCircle();
                UpdateCircles();
            }

            if (GUILayout.Button("Delete circle"))
            {
                _analyticView.RemoveLastCircle();
            }

            if (GUILayout.Button("Remove all circles"))
            {
                _analyticView.ClearCirclesArray();
            }

            base.OnInspectorGUI();
        }
    }
}