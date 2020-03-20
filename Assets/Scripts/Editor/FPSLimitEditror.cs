using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FpsLimit))]
public class FPSLimitEditror : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Change FPS"))
        {
            ((FpsLimit) target).ChangeFrameRate();
        }

        base.OnInspectorGUI();
    }
}