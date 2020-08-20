using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using ViewModels.UI.Elements.Buttons;

public static class HierarchyMenuObjects
{
#if UNITY_EDITOR

    private const string ResizableAreaImageButtonPrefabPath = "Assets/Prefabs/UI/Buttons/ResizableAreaImageButton.prefab";
    private const string NeutralButtonPath = "Assets/Prefabs/Common/Buttons/[Button] Neutral.prefab";
    private const string AccentButtonPath = "Assets/Prefabs/Common/Buttons/[Button] Accent.prefab";


    private static Object InstantiatePrefab(string path)
    {
        var selectedObject = Selection.activeGameObject;

        var loadedPrefab = AssetDatabase.LoadAssetAtPath(path, typeof(GameObject));

        if (selectedObject)
        {
            PrefabUtility.InstantiatePrefab(loadedPrefab, selectedObject.transform);
        }
        else
        {
            PrefabUtility.InstantiatePrefab(loadedPrefab, SceneManager.GetActiveScene());
        }

        return loadedPrefab;
    }

    [MenuItem("GameObject/UI/" + nameof(BarButtonSelection), false, 0)]
    private static void Init()
    {
        InstantiatePrefab(ResizableAreaImageButtonPrefabPath);
    }

    [MenuItem("GameObject/ChipIn/UI/" + "NeutralButton", false, 0)]
    public static void CreateDefaultSecondaryButton()
    {
        InstantiatePrefab(NeutralButtonPath);
    }

    [MenuItem("GameObject/ChipIn/UI/" + "AccentButton", false, 0)]
    public static void CreateAccentButton()
    {
        InstantiatePrefab(AccentButtonPath);
    }

#endif
}