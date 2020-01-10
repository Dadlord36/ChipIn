using Repositories;
using Repositories.Local;
using UI;
using UnityEditor;
using UnityEngine;

namespace Inspectors
{
    [CustomEditor(typeof(ImagesRoll))]
    public class ImagesRollEditor : Editor
    {
        private IconEllipseType _ellipseType;
        
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _ellipseType = (IconEllipseType) EditorGUILayout.EnumPopup(_ellipseType);
            
            if (GUILayout.Button("Generate Images"))
            {
                ((ImagesRoll) target).GenerateImages(_ellipseType);
                serializedObject.Update();
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
            }
        }
    }
}