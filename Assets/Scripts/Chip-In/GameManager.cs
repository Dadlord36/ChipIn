using System;
using System.Threading.Tasks;
using ActionsTranslators;
using Controllers;
using Factories;
using HttpRequests;
using Notifications;
using ScriptableObjects.CardsControllers;
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
    [SerializeField] private ApplicationClosingEventTranslator applicationClosingEventTranslator;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Canvas mainCanvas;
    [SerializeField] private AwaitingProcessVisualizerControllerScriptable awaitingProcessVisualizerControllerScriptable;

    public static Canvas MainCanvas { get; private set; }
    public static Camera MainCamera { get; private set; }
    public static Vector2 OriginalResolution { get; private set; } = new Vector2(375, 815);
    public static Vector2 ScreenResolutionScale { get; private set; }
    public static TaskScheduler MainThreadScheduler { get; private set; }
    public static AwaitingProcessVisualizerControllerScriptable MainAwaitingProcessVisualizerControllerScriptable { get; private set; }

    private void Awake()
    {
        MainThreadScheduler = TaskScheduler.FromCurrentSynchronizationContext();
    }

    private async void Start()
    {
        MainCamera = mainCamera;
        MainCanvas = mainCanvas;
        MainAwaitingProcessVisualizerControllerScriptable = awaitingProcessVisualizerControllerScriptable;
        ScreenResolutionScale = OriginalResolution / ScreenUtility.GetScreenSize();
        SimpleAutofac.AddObjectInstance(sessionController);
        
        try
        {
            await Initialize();
        }
        catch (Exception e)
        {
            LogUtility.PrintLogException(e);
            throw;
        }

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 1;
        FireBaseNotificationsController.Initialize();
    }

    private async Task Initialize()
    {
        InitializeSystems();

        try
        {
            await InitializeControllers();
            LogUtility.PrintLog(Tag, ScreenUtility.GetScreenSize().ToString());
            FireBaseNotificationsController.Dispose();
        }
        catch (Exception e)
        {
            LogUtility.PrintLogException(e);
            throw;
        }
    }

    private async Task InitializeControllers()
    {
        sessionController.ProcessAppLaunching();
        try
        {
            await restorationController.InvokeInterfaceMainFunction();
        }
        catch (Exception e)
        {
            LogUtility.PrintLogException(e);
            throw;
        }
    }

    private void InitializeSystems()
    {
        viewsPlacer.Initialize();
        ApiHelper.InitializeClient();
    }

    private async void OnApplicationQuit()
    {
        ApiHelper.Close();
        try
        {
            await applicationClosingEventTranslator.InvokeInterfaceMainFunction();
        }
        catch (Exception e)
        {
            LogUtility.PrintLogException(e);
            throw;
        }
    }

    private void Update()
    {
        mainUpdatableGroupController.Update();
    }
}