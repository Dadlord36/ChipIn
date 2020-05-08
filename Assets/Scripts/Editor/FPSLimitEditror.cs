using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FpsLimiter))]
public class FPSLimitEditror : Editor
{
    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Change FPS"))
        {
            ((FpsLimiter) target).ChangeFrameRate();
        }

        base.OnInspectorGUI();
    }
}