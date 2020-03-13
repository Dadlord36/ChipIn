using ActionsTranslators;
using Controllers;
using HttpRequests;
using UnityEngine;
using Utilities;
using Views.ViewElements.ViewsPlacers;

public class GameManager : MonoBehaviour
{
    private const string Tag = nameof(GameManager);
    
    [SerializeField] private SessionController sessionController;
    [SerializeField] private MainInputActionsTranslator inputActionsTranslator;
    [SerializeField] private TwoSlotsViewsPlacer viewsPlacer;
    [SerializeField] private DataRestorationController restorationController;

    private IUpdatable _updatable;

    // Start is called before the first frame update

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        _updatable = inputActionsTranslator;
        InitializeSystems();
        InitializeControllers();
        LogUtility.PrintLog(Tag,ScreenUtility.GetScreenSize().ToString());
    }

    private void InitializeControllers()
    {
        sessionController.ProcessAppLaunching();
        restorationController.Restore();
    }
    
    private void InitializeSystems()
    {
        viewsPlacer.Initialize();
        ApiHelper.InitializeClient();
    }

    private void OnApplicationQuit()
    {
        ApiHelper.Dispose();
    }

    private void Update()
    {
        _updatable.Update();
    }
}