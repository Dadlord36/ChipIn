using UnityEditor;
using UnityEngine;
using Views.ViewElements;

namespace Inspectors
{
    [CustomEditor(typeof(ViewSlot))]
    public class ViewSlotEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Log rect size"))
            {
                Debug.Log(((RectTransform) ((ViewSlot) target).transform).rect.ToString());
            }

            base.OnInspectorGUI();
        }
    }
}