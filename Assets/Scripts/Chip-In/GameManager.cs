﻿using System;
using System.Threading.Tasks;
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
    [SerializeField] private ApplicationClosingEventTranslator applicationClosingEventTranslator;
    [SerializeField] private ViewsLogoController viewsLogoController;

    private async void Start()
    {
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
        viewsLogoController.SetDefaultLogo();
    }

    private async Task Initialize()
    {
        InitializeSystems();

        try
        {
            await InitializeControllers();
        }
        catch (Exception e)
        {
            LogUtility.PrintLogException(e);
            throw;
        }

        LogUtility.PrintLog(Tag, ScreenUtility.GetScreenSize().ToString());
        FireBaseNotificationsController.Dispose();
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