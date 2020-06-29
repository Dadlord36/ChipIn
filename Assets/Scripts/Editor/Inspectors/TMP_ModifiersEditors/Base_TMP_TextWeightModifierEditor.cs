using Common.TMP_Modifiers;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Inspectors.TMP_ModifiersEditors
{
    public abstract class Base_TMP_TextWeightModifierEditor<T> : Editor where T : BaseTMP_TextWeightModifier
    {
        private T textWeightModifier;
        private const FontWeight DefaultFontWeight = FontWeight.SemiBold;
        private readonly string DefaultFontWeightName = DefaultFontWeight.ToString();

        private void OnEnable()
        {
            textWeightModifier = target as T;
        }

        public override void OnInspectorGUI()
        {
            EditorGUI.BeginChangeCheck();
            textWeightModifier.FieldFontWeight = (FontWeight) EditorGUILayout.EnumPopup("Font Weight", textWeightModifier.FieldFontWeight);

            if (GUILayout.Button($"Make it {DefaultFontWeightName}"))
            {
                textWeightModifier.FieldFontWeight = DefaultFontWeight;
            }

            if (EditorGUI.EndChangeCheck())
            {
                EditorUtility.SetDirty(target);
            }

            base.OnInspectorGUI();
        }
    }
}