using Controllers;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SlotsLauncher))]
public class SlotsLauncherEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        if (GUILayout.Button("Start spinning"))
        {
            ((SlotsLauncher)target)?.StartSpinning();
        }
        
        base.OnInspectorGUI();
    }
}