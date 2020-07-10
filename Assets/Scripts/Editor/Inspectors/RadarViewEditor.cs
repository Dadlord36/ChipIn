using UnityEditor;
using UnityEngine;
using ViewModels.UI.Elements;
using Views.ViewElements;

namespace Inspectors
{
    [CustomEditor(typeof(Radar))]
    public class RadarViewEditor : Editor
    {
        private Radar _analytic;

        private void OnEnable()
        {
            _analytic = (Radar) target;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();

            _analytic.circlesThickness =
                (uint) EditorGUILayout.IntField("Circle thickness", (int) _analytic.circlesThickness);
            _analytic.arcSteps =
                (uint) EditorGUILayout.IntField("Arc steps", (int) _analytic.arcSteps);
            _analytic.circlesColor = EditorGUILayout.ColorField("Circles color", _analytic.circlesColor);
            _analytic.scaleFactor = EditorGUILayout.Slider("Scale factor", _analytic.scaleFactor, 0f, 1f);
            _analytic.circlesBaseSize = EditorGUILayout.IntField("Circles size", _analytic.circlesBaseSize);

            void UpdateCircles()
            {
                _analytic.SetCirclesStyle(false, _analytic.circlesBaseSize,_analytic.arcSteps, _analytic.circlesThickness,
                    _analytic.circlesColor);
                _analytic.ScaleCircles(_analytic.scaleFactor);
            }

            if (EditorGUI.EndChangeCheck())
            {
                UpdateCircles();
                EditorUtility.SetDirty(_analytic);
            }

            if (GUILayout.Button("Add circle"))
            {
                _analytic.AddCircle();
                UpdateCircles();
            }

            if (GUILayout.Button("Delete circle"))
            {
                _analytic.RemoveLastCircle();
            }

            if (GUILayout.Button("Remove all circles"))
            {
                _analytic.ClearCirclesArray();
            }

            base.OnInspectorGUI();
        }
    }
}