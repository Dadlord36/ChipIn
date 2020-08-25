using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using ViewModels.UI.Elements.Buttons;

public static class HierarchyMenuObjects
{
    private const string RootMenuPath = "GameObject/ChipIn_UI/";

    private const string ButtonsCategory = "Buttons/";
    private const string InputFieldsCategory = "InputFields/";
    private const string TabsElementsCategory = "TabsElements/";
    private const string DelegationElementsCategory = "DelegationElements/";
    private const string DropdownsCategory = "Dropdowns/";
    private const string CoinsCategory = "Coins/";

    #region Prefabs Pathes

    private const string ResizableAreaImageButtonPrefabPath = "Assets/Prefabs/UI/Buttons/ResizableAreaImageButton.prefab";

    private const string NeutralButtonPath = "Assets/Prefabs/Common/Buttons/[Button] Neutral.prefab";
    private const string AccentButtonPath = "Assets/Prefabs/Common/Buttons/[Button] Accent.prefab";
    private const string ClickableAreaPrefabPath = "Assets/Prefabs/Common/Buttons/[InteractiveArea] ClickableArea.prefab";
    private const string ButtonWithImagePrefabPath = "Assets/Prefabs/Common/Buttons/[Button-Image].prefab";
    private const string AccentButtonWithCoinsCount = "Assets/Prefabs/Common/Coins/[Button] Accent With CoinsCounter.prefab";

    private const string MultiOptionsSelectionPanelPrefabPath = "Assets/Prefabs/Common/OptionsSelection/[Selection Panel] MultiOptionsSelectionPanel.prefab";
    private const string SelectableIconPrefabPath = "Assets/Prefabs/MerchantApp/Common/RelatedIconWithView.prefab";

    private const string SingleLineInputFiledPrefabPath = "Assets/Prefabs/Common/FormsElements/InputFieds/[InputField] DefaultSingleLineInputField.prefab";
    private const string MultiLineInputFiledPrefabPath = "Assets/Prefabs/Common/FormsElements/InputFieds/[InputField] DefaultMultiLineInputField.prefab";
    private const string TitledSingeLineInputFieldPrefabPath = "Assets/Prefabs/MerchantApp/Common/TitledSingleLineInputField.prefab";

    private const string DropdownTriggerPrefabPath = "Assets/Prefabs/Common/FormsElements/DropdownTrigger.prefab";

    private const string CoinsCountPrefabPath = "Assets/Prefabs/Common/Coins/CoinsCount.prefab";
    private const string CoinsCountWithDifferencePrefabPath = "Assets/Prefabs/Common/Coins/CoinsCountWithDifference.prefab";

    #endregion


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

    [MenuItem(RootMenuPath + ButtonsCategory + nameof(BarButtonSelection), false, 0)]
    public static void Init()
    {
        InstantiatePrefab(ResizableAreaImageButtonPrefabPath);
    }


    [MenuItem(itemName: RootMenuPath + ButtonsCategory + "NeutralButton", false, 0)]
    public static void CreateDefaultSecondaryButton()
    {
        InstantiatePrefab(NeutralButtonPath);
    }

    [MenuItem(RootMenuPath + ButtonsCategory + "AccentButton", false, 0)]
    public static void CreateAccentButton()
    {
        InstantiatePrefab(AccentButtonPath);
    }

    [MenuItem(RootMenuPath + ButtonsCategory + "ClickableArea", false, 0)]
    public static void CreateClickableArea()
    {
        InstantiatePrefab(ClickableAreaPrefabPath);
    }

    [MenuItem(RootMenuPath + TabsElementsCategory + "MultiOptionsSelectionPanel", false, 0)]
    public static void CreateMultiOptionsSelectionPanel()
    {
        InstantiatePrefab(MultiOptionsSelectionPanelPrefabPath);
    }

    [MenuItem(RootMenuPath + DelegationElementsCategory + "SelectableIcon", false, 0)]
    public static void CreateSelectableIcon()
    {
        InstantiatePrefab(SelectableIconPrefabPath);
    }

    [MenuItem(RootMenuPath + InputFieldsCategory + "SingleLineInputField", false, 0)]
    public static void CreateSingleLineMuInputField()
    {
        InstantiatePrefab(SingleLineInputFiledPrefabPath);
    }

    [MenuItem(RootMenuPath + InputFieldsCategory + "MultiLineInputField", false, 0)]
    public static void CreateMultiLineInputField()
    {
        InstantiatePrefab(MultiLineInputFiledPrefabPath);
    }

    [MenuItem(RootMenuPath + InputFieldsCategory + "TitledSingeLineInputField", false, 0)]
    public static void CreateTitledSingeLineInputField()
    {
        InstantiatePrefab(TitledSingeLineInputFieldPrefabPath);
    }

    [MenuItem(RootMenuPath + ButtonsCategory + "ButtonWithImage", false, 0)]
    public static void CreateButtonWithImage()
    {
        InstantiatePrefab(ButtonWithImagePrefabPath);
    }

    [MenuItem(RootMenuPath + DropdownsCategory + "DropdownTrigger", false, 0)]
    public static void CreateDropdownTrigger()
    {
        InstantiatePrefab(DropdownTriggerPrefabPath);
    }

    [MenuItem(RootMenuPath + CoinsCategory + "CoinsCunt", false, 0)]
    public static void CreateCoinsCount()
    {
        InstantiatePrefab(CoinsCountPrefabPath);
    }  
    
    [MenuItem(RootMenuPath + CoinsCategory + "CoinsCuntWithDifference", false, 0)]
    public static void CreateCoinsCountWithDifference()
    {
        InstantiatePrefab(CoinsCountWithDifferencePrefabPath);
    }
    
    [MenuItem(RootMenuPath + CoinsCategory + "AccentButtonWithCoinsCount", false, 0)]
    public static void CreateAccentButtonWithCoinsCount()
    {
        InstantiatePrefab(AccentButtonWithCoinsCount);
    }
}