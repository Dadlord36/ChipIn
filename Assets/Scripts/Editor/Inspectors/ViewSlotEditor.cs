using UnityEditor;
using UnityEngine;
using Utilities;
using Views.ViewElements;

namespace Inspectors
{
    [CustomEditor(typeof(ViewSlot))]
    public class ViewSlotEditor : Editor
    {
        private const string Tag = nameof(ViewSlotEditor);
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Log rect size"))
            {
                LogUtility.PrintLog(Tag,((RectTransform) ((ViewSlot) target).transform).rect.ToString());
            }

            base.OnInspectorGUI();
        }
    }
}