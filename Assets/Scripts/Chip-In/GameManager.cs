using System;
using Controllers;
using HttpRequests;
using UnityEngine;
using UnityEngine.UI;
using ViewModels.SwitchingControllers;
using Views;

public class GameManager : MonoBehaviour
{
    [SerializeField] private BaseViewSwitchingController mainViewsSwitchingController;
    [SerializeField] private CachingController cachingController;
    [SerializeField] private CanvasScaler mainCanvasScaler;
    [SerializeField] private Vector2Int referenceResolution;

    // Start is called before the first frame update

    private void Start()
    {
        mainCanvasScaler.referenceResolution = referenceResolution;
        ApiHelper.InitializeClient();
        mainViewsSwitchingController.RequestSwitchToView(null,nameof(WelcomeView));
        cachingController.ClearCache();
    }

    /*private void Update()
    {
        Debug.Log(mainCanvasScaler.referenceResolution.ToString());
    }*/

    private void OnApplicationQuit()
    {
        ApiHelper.Dispose();
    }
}