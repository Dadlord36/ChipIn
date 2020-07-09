using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using ViewModels.UI.Elements.Buttons;

public static class HierarchyMenuObjects
{
    private const string ResizableAreaImageButtonPrefabPath =
        "Assets/Prefabs/UI/Buttons/ResizableAreaImageButton.prefab";

#if UNITY_EDITOR

    [MenuItem("GameObject/UI/" + nameof(BarButtonSelection), false, 0)]
    private static void Init()
    {
        var selectedObject = Selection.activeGameObject;

        var loadedPrefab = AssetDatabase.LoadAssetAtPath(ResizableAreaImageButtonPrefabPath, typeof(GameObject));

        if (selectedObject)
        {
            PrefabUtility.InstantiatePrefab(loadedPrefab, selectedObject.transform);
        }
        else
        {
            PrefabUtility.InstantiatePrefab(loadedPrefab, SceneManager.GetActiveScene());
        }
        
    }
#endif
}