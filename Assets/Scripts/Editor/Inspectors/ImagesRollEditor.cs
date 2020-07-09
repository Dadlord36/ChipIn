using System;
using Repositories.Local;
using UnityEditor;
using UnityEngine;
using ViewModels.UI;

namespace Inspectors
{
    [CustomEditor(typeof(ImagesRoll))]
    public class ImagesRollEditor : Editor
    {
        private int _selectedIndex;

        private ImagesRoll _imagesRoll;

        private void OnEnable()
        {
            _imagesRoll = target as ImagesRoll;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            _selectedIndex = EditorGUILayout.Popup("Ellipse Name", _selectedIndex, _imagesRoll.IconsEllipsesOptionsNames);

            if (GUILayout.Button("Generate Images"))
            {
                _imagesRoll.GenerateImages(_imagesRoll.IconsEllipsesOptionsNames[_selectedIndex]);
                serializedObject.Update();
                serializedObject.ApplyModifiedPropertiesWithoutUndo();
                EditorUtility.SetDirty(target);
            }
        }
    }
}