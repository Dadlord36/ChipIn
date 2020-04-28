using ActionsTranslators;
using Controllers;
using HttpRequests;
using Notifications;
using UnityEngine;
using Utilities;
using Views.ViewElements.ViewsPlacers;

public class GameManager : MonoBehaviour
{
    private const string Tag = nameof(GameManager);

    [SerializeField] private SessionController sessionController;
    [SerializeField] private UpdatableGroupController mainUpdatableGroupController;
    [SerializeField] private TwoSlotsViewsPlacer viewsPlacer;
    [SerializeField] private DataRestorationController restorationController;


    // Start is called before the first frame update

    private void Start()
    {
        Initialize();
        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        FireBaseNotificationsController.Initialize();
    }

    private void Initialize()
    {
        InitializeSystems();
        InitializeControllers();
        LogUtility.PrintLog(Tag, ScreenUtility.GetScreenSize().ToString());
        FireBaseNotificationsController.Dispose();
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
        mainUpdatableGroupController.Update();
    }
}